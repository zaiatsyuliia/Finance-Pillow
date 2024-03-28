console.log('Expense script loaded.');
// expenses.js

// Function to draw a column chart for monthly expenses
console.log('Month Daily:', monthDaily);
console.log('Six Month Monthly:', sixMonthMonthly);
console.log('Year Monthly:', yearMonthly);

google.charts.load('current', { packages: ['corechart'] });
google.charts.setOnLoadCallback(function () {
    drawMonthDaily(monthDaily, 'month');
    drawSixMonthsChart(sixMonthMonthly, '6month');
    drawYearlyChart(yearMonthly, 'year');
});

function drawMonthDaily(monthlyData, chartContainerId) {
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'day');
    data.addColumn('number', 'totalSum');
    data.

    monthlyData.forEach(function (item) {
        data.addRow([item.Month, item.TotalSum]);
    });

    var options = {
        title: 'Monthly Expenses',
        chartArea: { width: '50%' },
        hAxis: {
            title: 'Month'
        },
        vAxis: {
            title: 'Total Expense'
        }
    };

    var chart = new google.visualization.ColumnChart(document.getElementById(chartContainerId));
    chart.draw(data, options);
}

// Function to draw a column chart for 6-monthly expenses
function drawSixMonthsChart(sixMonthsData, chartContainerId) {
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Month');
    data.addColumn('number', 'Total Expense');

    sixMonthsData.forEach(function (item) {
        data.addRow([item.Month, item.TotalSum]);
    });

    var options = {
        title: '6-Monthly Expenses',
        chartArea: { width: '50%' },
        hAxis: {
            title: 'Month'
        },
        vAxis: {
            title: 'Total Expense'
        }
    };

    var chart = new google.visualization.ColumnChart(document.getElementById(chartContainerId));
    chart.draw(data, options);
}

// Function to draw a column chart for yearly expenses
function drawYearlyChart(yearlyData, chartContainerId) {
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Month');
    data.addColumn('number', 'Total Expense');
    data.addColumn()
    yearlyData.forEach(function (item) {
        data.addRow([item.Month, item.TotalSum]);
    });

    var options = {
        title: 'Yearly Expenses',
        chartArea: { width: '50%' },
        hAxis: {
            title: 'Month'
        },
        vAxis: {
            title: 'Total Expense'
        }
    };

    var chart = new google.visualization.ColumnChart(document.getElementById(chartContainerId));
    chart.draw(data, options);
}
