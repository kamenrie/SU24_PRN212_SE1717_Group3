﻿const ctx1 = document.getElementById("user-buy-most-chart").getContext("2d");
const labels = ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12'];
const data = {
    labels: labels,
    datasets: [{
        label: 'User Buy Most Chart',
        data: [65, 59, 80, 81, 56, 55, 40, 10, 42, 10, 100, 69],
        backgroundColor: [
            'rgba(255, 99, 132, 0.2)',
            'rgba(255, 159, 64, 0.2)',
            'rgba(255, 205, 86, 0.2)',
            'rgba(75, 192, 192, 0.2)',
            'rgba(54, 162, 235, 0.2)',
            'rgba(153, 102, 255, 0.2)',
            'rgba(201, 203, 207, 0.2)'
        ],
        borderColor: [
            'rgb(255, 99, 132)',
            'rgb(255, 159, 64)',
            'rgb(255, 205, 86)',
            'rgb(75, 192, 192)',
            'rgb(54, 162, 235)',
            'rgb(153, 102, 255)',
            'rgb(201, 203, 207)'
        ],
        borderWidth: 1
    }]
};

const userBuyMostChart = {
    type: 'bar',
    data: data,
    options: {
        scales: {
            y: {
                beginAtZero: true
            }
        }
    }
};

new Chart(ctx1, userBuyMostChart);