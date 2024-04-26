async function fetchExchangeRates() {
    const response = await fetch(`https://api.exchangerate-api.com/v4/latest/UAH`);
    const data = await response.json();

    return data.rates;
}

async function populateCurrencyList() {
    const exchangeRates = await fetchExchangeRates();
    const currencySelect = $("#currency");

    for (const currency in exchangeRates) {
        if (exchangeRates.hasOwnProperty(currency) && !['RUB', 'BYN'].includes(currency)) {
            currencySelect.append(`<option value="${currency}">${currency}</option>`);
        }
    }
}

async function convertCurrency() {
    const uahAmount = parseFloat($("#amount").val());
    const selectedCurrency = $("#currency").val();
    const exchangeRates = await fetchExchangeRates();

    let convertedAmount = '';

    if (selectedCurrency) {
        convertedAmount = (uahAmount * exchangeRates[selectedCurrency]).toFixed(2);
        $(".convertedAmount").html(`<p><span class="amount">${convertedAmount}</span> <span class="currency">${selectedCurrency}</span></p>`);
    } else {
        let convertedAmounts = '<div class="row">';

        let count = 0;
        for (const currency in exchangeRates) {
            if (exchangeRates.hasOwnProperty(currency) && !['RUB', 'BYN'].includes(currency)) {
                const amount = (uahAmount * exchangeRates[currency]).toFixed(2);
                convertedAmounts += `<div class="column"><p><span class="amount">${amount}</span> <span class="currency">${currency}</span></p></div>`;
                count++;

                if (count % 4 === 0) {
                    convertedAmounts += '</div><div class="row">';
                }
            }
        }

        convertedAmounts += '</div>';
        convertedAmount = convertedAmounts;
        $(".convertedAmount").html(convertedAmount);
    }
}

$(document).ready(function () {
    populateCurrencyList();
});
