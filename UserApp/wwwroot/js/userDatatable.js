$(document).ready(function () {
    $("#userDatatable").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/User/loadData",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [{
            "targets": [0],
            "visible": false,
            "searchable": false
        }],
        "columns": [
            { "data": "id", "name": "Id", "autoWidth": true },
            { "data": "name", "name": "Name", "autoWidth": true },
            { "data": "dateOfBirth", "name": "Date Of Birth", "autoWidth": true },
            { "data": "married", "name": "Married", "autoWidth": true },
            { "data": "phone", "name": "Phone", "autoWidth": true },
            { "data": "salary", "name": "Salary", "autoWidth": true }, 
            {
                "render": function (data,row) { return "<a href='#' class='btn btn-danger' onclick=DeleteCustomer('" + row.id+ "'); >Delete</a>";   }
            },
        ]
    });
});  