// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Check connect Js
console.log("It Connect OK ");

// Function ค้นหา หน้าข้อมูลผู้เล่น
document.getElementById('searchInput').addEventListener('input', function () {
    let searchQuery = this.value.toLowerCase();
    let rows = document.querySelectorAll('#userTable tbody tr');
    rows.forEach(function (row) {
        let cells = row.querySelectorAll('td');
        let found = false;
        cells.forEach(function (cell) {
            if (cell.innerText.toLowerCase().includes(searchQuery)) {
                found = true;
            }
        });
        row.style.display = found ? '' : 'none';
    });
});


// Funciton นับเวลา Reload page website 

var startTime = performance.now(); // Capture start time
window.onload = function () {
    var endTime = performance.now(); // Capture end time after page load
    var loadTime = (endTime - startTime) / 1000; // Calculate time in seconds

    // Format to 2 decimal places
    var loadTimeInSeconds = loadTime.toFixed(2);

    // Display the load time in seconds with two decimals
    document.getElementById('loadTime').innerText = "Page loaded in " + loadTimeInSeconds + " seconds";
};


function Edit(identifier, updatedData) {
    $.ajax({
        url: '/Management/Player_info/Edit', // URL ไปยัง Action Method
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify({
            id: identifier,        // ส่ง id
            updatedData: updatedData // ส่งข้อมูลที่แก้ไขไป
        }),
        success: function (response) {
            if (response.success) {
                alert('ข้อมูลถูกแก้ไขเรียบร้อย');
            } else {
                alert('เกิดข้อผิดพลาด: ' + response.message);
            }
        },
        error: function (xhr, status, error) {
            alert('เกิดข้อผิดพลาดในการแก้ไขข้อมูล');
        }
    });
}


console.log('Data to send:', JSON.stringify({
    id: identifier,
    updatedData: updatedData
}));

function openEditModal(identifier, name, email) {
    // ใส่ข้อมูลลงในฟอร์ม
    $('#nameInput').val(name);
    $('#emailInput').val(email);

    // บันทึก identifier ไว้ใน attribute ของปุ่มบันทึก
    $('#saveChangesButton').data('identifier', identifier);

    // เปิด Modal
    $('#editModal').modal('show');
}