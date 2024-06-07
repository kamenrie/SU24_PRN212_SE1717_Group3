document.querySelector('.switch-to-register').addEventListener('click', function (e) {
    e.preventDefault();
    document.getElementById('login-box').style.display = 'none';
    document.getElementById('register-box').style.display = 'block';
});

document.querySelector('.switch-to-login').addEventListener('click', function (e) {
    e.preventDefault();
    document.getElementById('register-box').style.display = 'none';
    document.getElementById('login-box').style.display = 'block';
});
document.getElementById('forgot-link').addEventListener('click', function (e) {
    e.preventDefault();
    document.getElementById('login-box').style.display = 'none';
    document.getElementById('register-box').style.display = 'none';
    document.getElementById('forgot-box').style.display = 'block';
});

document.getElementById('back-to-login-2').addEventListener('click', function (e) {
    e.preventDefault();
    document.getElementById('register-box').style.display = 'none';
    document.getElementById('login-box').style.display = 'block';
    document.getElementById('forgot-box').style.display = 'none';
}); 