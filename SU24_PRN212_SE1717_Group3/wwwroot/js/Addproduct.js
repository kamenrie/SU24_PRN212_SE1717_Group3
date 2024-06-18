function triggerFileInput() {
    document.getElementById('imageInput').click();
}

function handleFileChange() {
    var fileInput = document.getElementById('imageInput');
    var ProductImage = document.getElementById('ProductImage');
    var imageName = document.getElementById('imageName');

    if (fileInput.files && fileInput.files[0]) {
        var reader = new FileReader();
        console.log(fileInput.files[0]);
        reader.onload = function (e) {
            ProductImage.src = e.target.result;
            imageName.innerText = fileInput.files[0].name;
        };

        reader.readAsDataURL(fileInput.files[0]);
    } else {
        ProductImage.src = "";
        imageName.innerText = "";
    }
}
