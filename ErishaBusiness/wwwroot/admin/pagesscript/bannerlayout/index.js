$(document).ready(function () {
    $("#bannerLayoutTable").DataTable({
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "ajax": {
            "url": "/admin/BannerLayout/LoadBannerLayoutData",
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
            { "data": "title", "name": "title", "autoWidth": true },
            {
                "bSortable": false, "bSearchable": false, "render": function (data, type, full, meta) { return (full.layoutType == 1 ? "Top" : (full.layoutType == 2 ? "Middle" : (full.layoutType == 3 ? "Slight Middle" : (full.layoutType == 4 ? "Bottom" : "")))); }
            },
            { "data": "categoryName", "name": "categoryName", "autoWidth": true }, 
            {
                "bSortable": false, "bSearchable": false, "render": function (data, type, full, meta) { return '<img src="' + full.imageUrl + '"/>'; }
            },
            {
                "bSortable": false, "bSearchable": false, "render": function (data, type, full, meta) { return '<span class="btn btn-info">' + (full.isActive == true? "Yes":"No")+'</span>'; }
            },
            {
                "bSortable": false, "bSearchable": false, "render": function (data, type, full, meta) { return '<a class="btn btn-info" href="/admin/bannerlayout/edit/' + full.id + '" title="Edit"><i class="mdi mdi-table-edit mx-0"></i></a> &nbsp<a class="btn btn-info" href="#"  title="Delete" onclick="DeleteData(' + full.id + ')"> <i class="mdi mdi-delete mx-0"></i></a>'; }
            }
        ]
    });
});


async function DeleteData(Id) {
    await ShowDeleteAlert();
    if (deleteStatus == true) {
        $('.overlay').show();
        $.get("/admin/bannerlayout/DeleteBannerLayoutById?Id=" + Id, function (data) {
            $('.overlay').hide();
            ShowSuccessAlert(data.data,'');
            setTimeout(function () {
                $('#bannerLayoutTable').DataTable().ajax.reload();
            }, 1000);
        });
    }
}
