let input = document.getElementById("searchbar");
let table = document.getElementById("searchTable");
let rows = table.getElementsByTagName("tr");
input.addEventListener("input", function () {
    let filter = input.value.toLowerCase();

    for (let i = 1; i < rows.length; i++) {
        let row = rows[i];
        let rowData = row.getElementsByTagName("td");
        let nameData = rowData[0].innerText.toLowerCase();
        let idData = rowData[1].innerText.toLowerCase();

        if (nameData.indexOf(filter) < 0 && idData.indexOf(filter) < 0)
            row.style.display = "none";
        else
            row.style.display = "";
    }
});
