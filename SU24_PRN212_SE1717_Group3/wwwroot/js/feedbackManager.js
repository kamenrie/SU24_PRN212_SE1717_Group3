


    $(function () {
        var includes = $('[data-include]')
        $.each(includes, function () {
            var file = './layout/' + $(this).data('include') + '.html'
            $(this).load(file)
        })
    })

    var today = new Date();

    var hours = today.getHours();
    var minutes = today.getMinutes();
    var seconds = today.getSeconds();
    var day = today.getDate();
    var month = today.getMonth() + 1;
    var year = today.getFullYear();

    if (hours < 10) hours = "0" + hours;
    if (minutes < 10) minutes = "0" + minutes;
    if (seconds < 10) seconds = "0" + seconds;

    var currentDate = day + "/" + month + "/" + year;
    var currentTime = hours + ":" + minutes + ":" + seconds;

    document.getElementById("time-1").innerHTML = currentTime;
    document.getElementById("date-1").innerHTML = currentDate;
    document.getElementById("time-2").innerHTML = currentTime;
    document.getElementById("date-2").innerHTML = currentDate;

    function toggleTextarea(id) {
        var reply = document.querySelector('#feedback-reply-' + id);
        if (reply.style.display == 'none') {
            reply.style.display = 'block';

        } else {
            reply.style.display = 'none';
        }
    }
