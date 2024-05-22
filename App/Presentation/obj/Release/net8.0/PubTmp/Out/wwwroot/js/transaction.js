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

    // Add new options
    categories.forEach(function (category) {
        var option = document.createElement('option');
        option.value = parseInt(category.categoryId);
        option.textContent = category.name;
        selectElement.appendChild(option);
    });
});