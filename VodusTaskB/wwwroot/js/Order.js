$(document).ready(function () {
    console.log('ok');
    var currentDate = new Date().toISOString().slice(0, 10);
    $('#searchInput').val('');
    var value = localStorage.getItem("selectedValue");
    if (value == null) {

        $('#dataSourceSelector').val("1");

    }
    else
    {
        $('#dataSourceSelector').val(value);
    }
    assignLocalStorageValue();

    $('#dataSourceSelector').change(function () {
        localStorage.setItem("selectedValue", this.value);
        reloadPage();
    });
    // Search input box keyup event
    $('#searchInput').keyup(function () {
        localStorage.setItem("searchInput", this.value);
        reloadPage();
    });
    //$('.start-date-picker').datepicker({
    //    format: 'dd-M-yyyy', // specify the desired format
    //    autoclose: true // close the datepicker when a date is selected
    //});

    //// Initialize end date picker
    //$('.end-date-picker').datepicker({
    //    format: 'dd-M-yyyy', // specify the desired format
    //    autoclose: true // close the datepicker when a date is selected
    //});
    // Date range picker change event
    $(".fromDate").on("change", function () {
        reloadPage();
    });
    function reloadPage() {
        var loadFromJson = $('#dataSourceSelector').val() === "2";
        var searchQuery = $('#searchInput').val();
        var dateRange = $('#dateRangePicker').val();
        //var dates = dateRange.split(' - ');
        var fromDate = formatDateToUTC($("#startTimestamp").val()).toLocaleString();
        var toDate = formatDateToUTC($("#endTimestamp").val()).toLocaleString();

        window.location.href = '/Order/Index?searchQuery=' + searchQuery + '&fromDate=' + fromDate + '&toDate=' + toDate + '&loadFromJson=' + loadFromJson;

    }
    $('#startTimestamp').change(function ()
    {
        localStorage.setItem("fromDateValue", $("#startTimestamp").val());
        reloadPage();

    });
    $('#endTimestamp').change(function ()
    {
        
        if (checkDate() == true)
        {
            alert("Start date cannot be greater than end date.");
            return;
        }
        else
        {
            localStorage.setItem("toDateValue", $("#endTimestamp").val());
            reloadPage();
        }

    });
    function assignLocalStorageValue()
    {
        var fromDate =  localStorage.getItem("fromDateValue");
        var toDate = localStorage.getItem("toDateValue");
        var searchInput = localStorage.getItem("searchInput");
        $('#searchInput').val(searchInput);
        $("#startTimestamp").val(fromDate);
        $("#endTimestamp").val(toDate);
    }
    function checkDate() {
        var startDate = new Date(document.getElementById("startTimestamp").value);
        var endDate = new Date(document.getElementById("endTimestamp").value);

        if (startDate > endDate) {
            
            return true;
        }
    }

    function formatDate(date) {
        // Get day, month, and year
        var day = date.getDate();
        var month = date.toLocaleString('default', { month: 'short' });
        var year = date.getFullYear();
        return day + '-' + month + '-' + year;
    }
    function formatDateToUTC(date) {
        var dateParts = date.split('-');
        if (dateParts.length === 3) {
            var day = dateParts[2];
            var month = dateParts[1];
            var year = dateParts[0];
            var utcDate = new Date(Date.UTC(year, month - 1, day));
            return utcDate.toISOString().split('T')[0];
        }
        return date;
    }
    const getCellValue = (tr, idx) =>
        tr.children[idx].innerText || tr.children[idx].textContent;

    const comparer = (idx, asc) => (a, b) =>
        ((v1, v2) =>
            v1 !== '' && v2 !== '' && !isNaN(v1) && !isNaN(v2)
                ? v1 - v2
                : v1.toString().localeCompare(v2))(
                    getCellValue(asc ? a : b, idx),
                    getCellValue(asc ? b : a, idx)
                );

    // do the work...
    document.querySelectorAll('#orderTable th').forEach((th, index) =>
        th.addEventListener('click', function () {
            const table = th.closest('table');
            const asc = this.asc = !this.asc;
            const headerRow = table.querySelector('tr:first-child'); // Get the header row
            const rows = Array.from(table.querySelectorAll('tr')).slice(1); // Exclude the header row
            rows.sort(comparer(index, asc)).forEach((tr) => table.appendChild(tr)); // Append sorted rows after the header row
            const sortedColumn = document.querySelectorAll("#orderTable th")[index];
            const sortedIndicators = document.querySelectorAll("#orderTable .sorted");
            sortedIndicators.forEach(indicator => indicator.classList.remove("sorted-asc", "sorted-desc"));
        })
    );


});
