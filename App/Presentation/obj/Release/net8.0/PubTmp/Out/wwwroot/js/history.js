document.getElementById('allHistoryBtn').addEventListener('click', async () => {
    await updateHistory('all');
});

document.getElementById('expenseHistoryBtn').addEventListener('click', async () => {
    await updateHistory('expense');
});

document.getElementById('incomeHistoryBtn').addEventListener('click', async () => {
    await updateHistory('income');
});

document.getElementById('filterByDateBtn').addEventListener('click', async () => {
    const startDate = document.getElementById('startDate').value;
    const endDate = document.getElementById('endDate').value;
    await updateHistory('date', startDate, endDate);
});

async function updateHistory(filterType, startDate = null, endDate = null) {
    let url = '/Home/FilteredHistory?filterType=' + filterType;

    if (startDate && endDate) {
        url += '&startDate=' + startDate + '&endDate=' + endDate;
    }

    const response = await fetch(url);
    const historyHtml = await response.text();

    document.querySelector('.transactionHistory table').innerHTML = historyHtml;
}