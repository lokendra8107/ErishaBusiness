$(document).ready(function () {
    $("#EmailTemplateTable").DataTable({
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "ajax": {
            "url": "/admin/EmailTemplate/LoadEmailTemplatedData",
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
            { "data": "emailTitle", "name": "emailTitle", "autoWidth": true },
            { "data": "emailSubject", "name": "emailSubject", "autoWidth": true },
            { "data": "emailFrom", "name": "emailFrom", "autoWidth": true },
            {
                "bSortable": false, "bSearchable": false, "render": function (data, type, full, meta) { return '<span class="btn btn-info">' + (full.isActive == true? "Yes":"No")+'</span>'; }
            },
            {
                "bSortable": false, "bSearchable": false, "render": function (data, type, full, meta) { return '<a class="btn btn-info" href="/admin/emailtemplate/edit/' + full.id + '" title="Edit"><i class="mdi mdi-table-edit mx-0"></i></a>'; }
            }
        ]
    });
});
