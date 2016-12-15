$(document).ready(function () {
    $('#gridUser').jqGrid({
        colNames: ['UserId', 'IsActive', 'RegDate', 'Country', 'TimeZone'],
        colModel: [
                    { name: 'UserId', width: 100, index: 'UserId', searchoptions: { sopt: ['eq', 'ne'] } },
                    { name: 'IsActive', width: 100, index: 'IsActive', searchoptions: { sopt: ['eq', 'ne'] } },
                    { name: 'RegDate', index: 'RegDate', searchoptions: { sopt: ['eq', 'ne', 'cn'] } },
                    { name: 'Country', index: 'Country', searchoptions: { sopt: ['eq', 'ne', 'cn'] } },
                    { name: 'TimeZone', index: 'TimeZone', searchoptions: { sopt: ['eq', 'ne', 'cn'] } }
                  ],
        pager: jQuery('#pager'),
        sortname: 'UserId',
        rowNum: 10,
        rowList: [10, 20, 50],
        sortorder: "asc",
       /* width: 600,
        height: 250,*/
        datatype: 'json',
        caption: 'Result of scanning',
        viewrecords: true,
        mtype: 'GET',
        jsonReader: {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            userdata: "userdata"
        },
        //or ondblClickRow
        onSelectRow: function (id) {
            var row = $('#gridUser').getRowData(id);
            //GetInfo(row['Computer_Name'], row['Computer_IP']);
        },
        url: "/User/GetUsers"
    }).navGrid('#pager', { view: false, del: false, add: false, edit: false },
       {}, // default settings for edit
       {}, // default settings for add
       {}, // delete instead that del:false we need this
       {closeOnEscape: true, multipleSearch: true, closeAfterSearch: true }, // search options
       {} /* view parameters*/
     );
     
});