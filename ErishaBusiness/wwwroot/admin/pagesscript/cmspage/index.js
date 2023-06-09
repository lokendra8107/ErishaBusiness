$(document).ready(function () {
    $("#cmspageTable").DataTable({
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "ajax": {
            "url": "/admin/cmspage/LoadCmsPageData",
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
            { "data": "name", "name": "name", "autoWidth": true },
            { "data": "title", "name": "title", "autoWidth": true },
            { "data": "url", "name": "url", "autoWidth": true, "bSortable": false, "bSearchable": false },
            {
                "bSortable": false, "bSearchable": false, "render": function (data, type, full, meta) { return '<span class="btn btn-info">' + (full.isActive == true? "Yes":"No")+'</span>'; }
            },
            {
                "bSortable": false, "bSearchable": false, "render": function (data, type, full, meta) { return '<a class="btn btn-info" href="/admin/cmspage/edit/' + full.id + '" title="Edit"><i class="mdi mdi-table-edit mx-0"></i></a> &nbsp<a class="btn btn-info" href="#"  title="Delete" onclick="DeleteData(' + full.id + ')"> <i class="mdi mdi-delete mx-0"></i></a>'; }
            }
        ]
    });
});


async function DeleteData(Id) {
    await ShowDeleteAlert();
    if (deleteStatus == true) {
        $('.overlay').show();
        $.get("/admin/cmspage/DeleteCmsPageById?Id=" + Id, function (data) {
            $('.overlay').hide();
            ShowSuccessAlert(data.data,'');
            setTimeout(function () {
                $('#cmspageTable').DataTable().ajax.reload();
            }, 1000);
        });
    }
}
