console.log('Transaction form script loaded.');
console.log('Income Categories:', incomeCategories);
console.log('Expense Categories:', expenseCategories);

document.getElementById('type').addEventListener('change', function () {
    var type = this.value;
    var categories = [];

    // Check if type is Income or Expense
    if (type === 'Income') {
        // Use incomeCategories
        categories = incomeCategories;
    } else if (type === 'Expense') {
        // Use expenseCategories
        categories = expenseCategories;
    }

    // Clear existing options
    var selectElement = document.getElementById('categoryId');
    selectElement.innerHTML = '';
    console.log('Dropdown Element:', selectElement);

    // Add new options
    categories.forEach(function (category) {
        var option = document.createElement('option');
        option.value = category.idCategory;
        option.textContent = category.name;
        selectElement.appendChild(option);
    });
    console.log('Dropdown HTML:', selectElement.innerHTML);
});