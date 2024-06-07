// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener("DOMContentLoaded", function () {
    var subnavs = document.querySelectorAll('.subnav');
    var subnavsProfile = document.querySelectorAll('.subnav-profile');
    subnavs.forEach(function (subnav) {
        subnav.style.display = 'none';
    });
    subnavsProfile.forEach(function (subnav) {
        subnav.style.display = 'none';
    });


    var menus = document.querySelectorAll('.btn-Login');

    menus.forEach(function (menu) {
        menu.addEventListener('click', function () {
            var subnav = this.querySelector('.subnav');
            if (subnav.style.display === 'block') {
                subnav.style.display = 'none';
            } else {
                subnav.style.display = 'block';
            }
        });
    });
    menus.forEach(function (menu) {
        menu.addEventListener('click', function () {
            var subnav = this.querySelector('.subnav-profile');
            if (subnav.style.display === 'block') {
                subnav.style.display = 'none';
            } else {
                subnav.style.display = 'block';
            }
        });
    });
});

