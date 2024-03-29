﻿google.charts.load('current', { 'packages': ['corechart'] });
google.charts.setOnLoadCallback(drawChart);

function drawChart() {
    drawColumnChartForMonthDaily();
    drawPieChartForMonthTotal();
    drawColumnChartForSixMonthsMonthly();
    drawPieChartForSixMonthsTotal();
    drawColumnChartForYearMonthly();
    drawPieChartForYearTotal();
}

function drawColumnChartForMonthDaily() {
    var data = new google.visualization.DataTable();
    data.addColumn('date', 'Day');
    categories.forEach(function (category) {
        data.addColumn('number', category);
    });

    var grouped = {};
    monthDaily.forEach(function (entry) {
        var day = new Date(entry.day).toDateString();
        if (!grouped[day]) {
            grouped[day] = {};
            categories.forEach(function (category) {
                grouped[day][category] = 0;
            });
        }
        grouped[day][entry.categoryName] += entry.totalSum;
    });

    for (var day in grouped) {
        var row = [new Date(day)];
        categories.forEach(function (category) {
            row.push(grouped[day][category]);
        });
        data.addRow(row);
    }

    var options = {
        title: 'Month Daily',
        hAxis: {
            title: 'Day',
            format: 'dd MMM yyyy'
        },
        vAxis: {
            title: 'Total'
        },
        isStacked: true
    };

    var chart = new google.visualization.ColumnChart(document.getElementById('columnChartForMonthDaily'));
    chart.draw(data, options);
}

function drawPieChartForMonthTotal() {
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Category');
    data.addColumn('number', 'Total Sum');

    monthTotal.forEach(function (entry) {
        data.addRow([entry.categoryName, entry.totalSum]);
    });

    var options = {
        title: 'Month Total by Category',
    };

    var chart = new google.visualization.PieChart(document.getElementById('pieChartForMonthTotal'));
    chart.draw(data, options);
}

function drawColumnChartForSixMonthsMonthly() {
    var data = new google.visualization.DataTable();
    data.addColumn('date', 'Month');

    categories.forEach(function (category) {
        data.addColumn('number', category);
    });

    var grouped = {};
    sixMonthMonthly.forEach(function (entry) {
        var month = new Date(entry.month).toDateString();
        if (!grouped[month]) {
            grouped[month] = {};
            categories.forEach(function (category) {
                grouped[month][category] = 0;
            });
        }
        grouped[month][entry.categoryName] += entry.totalSum;
    });

    for (var month in grouped) {
        var row = [new Date(month)];
        categories.forEach(function (category) {
            row.push(grouped[month][category]);
        });
        data.addRow(row);
    }

    var options = {
        title: '6 Months Monthly',
        hAxis: {
            title: 'Month',
            format: 'MMM yyyy'
        },
        vAxis: {
            title: 'Total'
        },
        isStacked: true
    };

    var chart = new google.visualization.ColumnChart(document.getElementById('columnChartForSixMonthsMonthly'));
    chart.draw(data, options);
}

function drawPieChartForSixMonthsTotal() {
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Category');
    data.addColumn('number', 'Total Sum');

    sixMonthTotal.forEach(function (entry) {
        data.addRow([entry.categoryName, entry.totalSum]);
    });

    var options = {
        title: '6 Months Total by Category',
    };

    var chart = new google.visualization.PieChart(document.getElementById('pieChartForSixMonthsTotal'));
    chart.draw(data, options);
}

function drawColumnChartForYearMonthly() {
    var data = new google.visualization.DataTable();
    data.addColumn('date', 'Month');

    categories.forEach(function (category) {
        data.addColumn('number', category);
    });

    var grouped = {};
    yearMonthly.forEach(function (entry) {
        var month = new Date(entry.month).toDateString();
        if (!grouped[month]) {
            grouped[month] = {};
            categories.forEach(function (category) {
                grouped[month][category] = 0;
            });
        }
        grouped[month][entry.categoryName] += entry.totalSum;
    });

    for (var month in grouped) {
        var row = [new Date(month)];
        categories.forEach(function (category) {
            row.push(grouped[month][category]);
        });
        data.addRow(row);
    }

    var options = {
        title: 'Year Monthly',
        hAxis: {
            title: 'Month',
            format: 'MMM yyyy'
        },
        vAxis: {
            title: 'Total'
        },
        isStacked: true
    };

    var chart = new google.visualization.ColumnChart(document.getElementById('columnChartForYearMonthly'));
    chart.draw(data, options);
}

function drawPieChartForYearTotal() {
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Category');
    data.addColumn('number', 'Total Sum');

    yearTotal.forEach(function (entry) {
        data.addRow([entry.categoryName, entry.totalSum]);
    });

    var options = {
        title: 'Year Total by Category',
    };

    var chart = new google.visualization.PieChart(document.getElementById('pieChartForYearTotal'));
    chart.draw(data, options);
}