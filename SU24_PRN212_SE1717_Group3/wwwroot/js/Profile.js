function triggerFileInput() {
    // Trigger the hidden file input when the div is clicked
    document.getElementById('fileInput').click();
}

function handleFileChange() {
    var fileInput = document.getElementById('fileInput');
    var avatarImage = document.getElementById('avatarImage');

    if (fileInput.files && fileInput.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            avatarImage.src = e.target.result;
        };

        reader.readAsDataURL(fileInput.files[0]);
    }
}