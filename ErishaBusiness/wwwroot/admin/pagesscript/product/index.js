$(document).ready(function () {
    $("#ProductTable").DataTable({
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "ajax": {
            "url": "/admin/Product/LoadProductData",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs":
            [{
                "targets": [0],
                "visible": false,
                "searchable": false
            }],
        "columns": [ 
            { "data": "id", "name": "Id", "autoWidth": true },
            { "data": "productName", "name": "ProductName", "autoWidth": true },
            { "data": "categoryName", "name": "CategoryName", "autoWidth": true },
            { "data": "skuId", "name": "SKUId", "autoWidth": true },
            {
                "bSortable": false, "bSearchable": false, "render": function (data, type, full, meta) { return '<img src="' + full.productImage + '"/>'; }
            },
            {
                "bSortable": false, "bSearchable": false, "render": function (data, type, full, meta) { return '<span class="btn btn-info">' + (full.isActive == true? "Yes":"No")+'</span>'; }
            },
            {
                "bSortable": false, "bSearchable": false, "render": function (data, type, full, meta) { return '<a class="btn btn-info" href="/admin/product/edit/' + full.id + '" title="Edit"><i class="mdi mdi-table-edit mx-0"></i></a> &nbsp<a class="btn btn-info" href="#"  title="Delete" onclick="DeleteData(' + full.id + ')"> <i class="mdi mdi-delete mx-0"></i></a>'; }
            }
        ]
    });
});


async function DeleteData(Id) {
    await ShowDeleteAlert();
    if (deleteStatus == true) {
        $('.overlay').show();
        $.get("/admin/Product/DeleteProductById?Id=" + Id, function (data) {
            $('.overlay').hide();
            ShowSuccessAlert(data.data,'');
            setTimeout(function () {
                $('#ProductTable').DataTable().ajax.reload();
            }, 1000);
        });
    }
}
