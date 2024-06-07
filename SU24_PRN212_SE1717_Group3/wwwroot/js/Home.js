document.getelementbyid('showimagesbutton').addeventlistener('click', function () {
    var imagecontainer = document.getelementbyid('imagecontainer');

    // kiểm tra nếu các hình ảnh đã được hiển thị, nếu có thì ẩn đi, nếu không thì hiển thị
    if (imagecontainer.style.display === 'none') {
        imagecontainer.innerhtml = ''; // xóa hình ảnh hiện có (nếu có)
        var images = ['image1.jpg', 'image2.jpg', 'image3.jpg']; // đường dẫn tới các hình ảnh
        images.foreach(function (imagesrc) {
            var img = document.createelement('img');
            img.src = imagesrc;
            imagecontainer.appendchild(img);
        });
        imagecontainer.style.display = 'block';
    } else {
        imagecontainer.style.display = 'none';
    }
});
