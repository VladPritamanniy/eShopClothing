const file = (function () {
    return {
        addFormFileImages: function (fileInput, divForImg) {
            for (let i = 0; i < fileInput.files.length; i++) {
                let file = fileInput.files[i];
                if (file) {
                    let img = document.createElement("img");
                    let reader = new FileReader();
                    reader.onload = function (e) {
                        img.className = "form-file-image";
                        img.src = e.target.result;
                        divForImg.append(img);
                    };
                    reader.readAsDataURL(file);
                }
            }
        }
    };
})();