
    window.onload = function () {
            var error = `${error}`;
    if (error !== null && error !== "") {
        showNotification(error);
            }
        };

    function showNotification(message) {
            var notification = document.createElement('div');
    notification.className = 'notification';
    notification.innerHTML = "<i class='fa-solid fa-triangle-exclamation'></i> " + message;

    var style = document.createElement('style');
    style.innerHTML = `.notification {
        position: fixed;
    top: 5%;
    opacity: 0.9;
    left: 45%;
    width: 50%;
    padding: 15px;
    border-radius: 15px;
    border: none;
    background-color: #ff0000;
    color: #ffffff;
    text-align: center;
    font-weight: bold;
        }`;
    document.head.appendChild(style);

    document.body.appendChild(notification);

    setTimeout(function () {
        document.head.removeChild(style);
    document.body.removeChild(notification);
            }, 3000);
        }

    let body = document.querySelector('body');
    $(document).ready(function () {
        $("#firstForm").submit(function (event) {

            event.preventDefault();
            let emailValue = $("#emailInput").val();

            // Set the value to the hidden input in #secondForm
            $("#hiddenEmailInput").val(emailValue);

            $.ajax({
                type: "POST",
                url: "auth?action=forgotPassPost",
                data: $("#firstForm").serialize(),
                success: function (response) {
                    if (response.trim() !== "") {
                        showNotification(`This email is not existed or not a member in our website!`);
                    } else {
                        body.classList.add('otp');
                    }
                },
                error: function (error) {
                }
            });
        });
        });
