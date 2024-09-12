const search = (function() {
    return {
        getSuggestions: function() {
            var term = document.getElementById('searchInput').value;

            fetch('/index?handler=suggestions&term=' + encodeURIComponent(term))
                .then(function (response) {
                    return response.json();
                })
                .then(function (suggestions) {
                    createUl(suggestions);
                })
                .catch(function (error) {
                    console.error('Error fetching suggestions:', error);
                });
        },
        createUl: function (suggestions) {
            var suggestionsList = document.getElementById('suggestions');
            suggestionsList.innerHTML = '';
            suggestionsList.style.display = 'block';

            var json = JSON.parse(suggestions);

            let suggests = json.suggest["product-suggest"][0].options;
            let hits = json.hits.hits;
            if (suggests.length > 0) {
                suggests.slice(0, 5).forEach(item => {
                    let exists = Array.from(suggestionsList.querySelectorAll('li label.name')).some(label => label.textContent === item.text);

                    if (!exists) {
                        let li = document.createElement('li');
                        li.innerHTML = '<label class="name" id="suggestedName" onclick="selectSuggest(this)">' + item.text + '</label>';
                        suggestionsList.appendChild(li);
                    }
                });
            }
            hits.slice(0, 5).forEach(item => {
                if (item.highlight) {
                    let li = document.createElement('li');
                    let a = document.createElement('a');

                    let name = item.highlight.name !== undefined ? item.highlight.name[0] : item._source.name;
                    a.innerHTML += '<label class="name">' + name + '</label>';

                    let description = item.highlight.description !== undefined ? item.highlight.description[0] : '';
                    a.innerHTML += '<label class="description">' + description + '</label>';

                    a.href = window.location.origin + '/product/' + item._id;
                    li.innerHTML += '<hr>';

                    li.appendChild(a);
                    suggestionsList.appendChild(li);
                }
            });
            if (suggests.length <= 0 && hits.length <= 0) {
                suggestionsList.style.display = 'none';
            }
        },
        selectSuggest: function (suggestedName) {
            var searchInput = document.getElementById('searchInput');
            searchInput.value = suggestedName.textContent;
            getSuggestions();
        }
};
})();