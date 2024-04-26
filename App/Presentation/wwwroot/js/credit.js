function calculate() {
    var term = parseInt(document.getElementById("term").value);
    var amount = parseFloat(document.getElementById("amount").value);
    var rate = parseFloat(document.getElementById("rate").value);

    // Early return for edge cases
    if (term <= 0 || amount <= 0 || isNaN(rate)) {
        alert("Please enter valid input values.");
        return;
    }

    var monthlyRate = rate / 100 / 12;
    var denominator = Math.pow(1 + monthlyRate, -term);
    var monthlyPayment;

    if (rate === 0) {
        monthlyPayment = amount / term;
    } else {
        monthlyPayment = (amount * monthlyRate) / (1 - denominator);
    }

    var totalPayment = monthlyPayment * term;
    var totalInterest = totalPayment - amount;

    // Update the HTML elements with the calculated values
    document.getElementById("monthlyPayment").innerText = monthlyPayment.toFixed(2);
    document.getElementById("totalPayment").innerText = totalPayment.toFixed(2);
    document.getElementById("totalInterest").innerText = totalInterest.toFixed(2);
}