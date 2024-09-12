using Application.DTO;
using Application.Interfaces;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace Application.Services
{
    public class ElasticService : BackgroundService, IElasticService
    {
        private const string ElasticUrl = "http://localhost:9200/";
        private const string IndexName = "products";
        private readonly HttpClient _httpClient;
        private readonly ILogger<ElasticService> _logger;

        public ElasticService(ILogger<ElasticService> logger)
        {
            _httpClient = new HttpClient();
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await CreateIndex();
        }

        public async Task CreateIndex()
        {
            _logger.LogInformation("Start creating index");
            try
            {
                var indexExistsResponse = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, ElasticUrl + IndexName));

                if (indexExistsResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    _logger.LogInformation("Index already exists.");
                    return;
                }
                var json = @"
                {
                    ""settings"": {
                        ""analysis"": {
                            ""analyzer"": {
                                ""default"": {
                                    ""type"": ""standard""
                                }
                            }
                        }
                    },
                    ""mappings"": {
                        ""properties"": {
                            ""name"": {
                                ""type"": ""text""
                            },
                            ""description"": {
                                ""type"": ""text""
                            },
                            ""suggest"": {
                                ""type"": ""completion""
                            }
                        }
                    }
                }";

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync(ElasticUrl + IndexName, content);
                var result = await response.Content.ReadAsStringAsync();
                _logger.LogInformation(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public async Task IndexDocument(ClothingDto clothingDto)
        {
            _logger.LogInformation("Start indexing document");
            try
            {
                static string[] Split(string str)
                {
                    str = Regex.Replace(str, @"[^\w\s-]", "");
                    return str.Split(new[] { ' ' },
                        StringSplitOptions.RemoveEmptyEntries);
                };

                string[] suggestsFromDescription = Split(clothingDto.Description);
                string[] suggestsFromName = Split(clothingDto.Name);
                string[] combinedSuggests = suggestsFromDescription.Concat(suggestsFromName).Distinct().ToArray();

                var json = $@"
                {{
                    ""name"": {JsonConvert.SerializeObject(clothingDto.Name)},
                    ""description"": {JsonConvert.SerializeObject(clothingDto.Description)},
                    ""suggest"": {{
                        ""input"": {JsonConvert.SerializeObject(combinedSuggests)}
                    }}
                }}";

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(ElasticUrl + IndexName + "/_doc/" + clothingDto.Id, content);
                var result = await response.Content.ReadAsStringAsync();
                _logger.LogInformation(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public async Task<string> LiveSearch(string searchQuery)
        {
            try
            {
                var json = $@"
                {{
                    ""query"": {{
                        ""multi_match"": {{
                            ""query"": ""{searchQuery}"",
                            ""fields"": [""name"", ""description""],
                            ""type"": ""best_fields"",
                            ""operator"": ""or""
                        }}
                    }},
                    ""highlight"": {{
                        ""pre_tags"": [""<b>""],
                        ""post_tags"": [""</b>""],
                        ""fields"": {{
                            ""name"": {{}},
                            ""description"": {{}}
                        }}
                    }},
                    ""suggest"": {{
                        ""product-suggest"": {{
                            ""prefix"": ""{searchQuery}"",
                            ""completion"": {{
                                ""field"": ""suggest""
                            }}
                        }}
                    }}
                }}";

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(ElasticUrl + IndexName + "/_search", content);
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ex.Message;
            }
        }

        public async Task<string> SearchByQuery(string query)
        {
            var json = $@"{{
              ""query"": {{
                ""bool"": {{
                  ""should"": [
                    {{
                      ""match"": {{
                        ""name"": {{
                          ""query"": ""{query}"",
                          ""operator"": ""or""
                        }}
                      }}
                    }},
                    {{
                      ""match"": {{
                        ""description"": {{
                          ""query"": ""{query}"",
                          ""operator"": ""or""
                        }}
                      }}
                    }}
                  ]
                }}
              }},
              ""sort"": [
                {{
                  ""_score"": {{
                    ""order"": ""desc""
                  }}
                }}
              ]
            }}";

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(ElasticUrl + IndexName + "/_search", content);
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}
