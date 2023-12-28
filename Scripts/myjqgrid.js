$(document).ready(function () {
    $Grid = $("#grid").jqGrid({
        url: '/Customer/GetData',
        mtype: 'GET',
        datatype: 'json',
        contentType: "application/json; charset-utf-8",
        colNames: ['Id', 'FirstName', 'LastName', 'ContactNo', 'Email', 'UserId', 'Status', 'AccountsCount', 'DocumentsCount'],
        colModel: [
            { label: 'Id', name: 'Id', key: true, search: true },
            { label: 'FirstName', name: 'FirstName', editable: true, search: true },
            { label: 'LastName', name: 'LastName', editable: true, search: true },
            { label: 'ContactNo', name: 'ContactNo', editable: true, search: true },
            { label: 'Email', name: 'Email', editable: true, search: true },
            { label: 'UserId', name: 'UserId', search: false },
            { label: 'Status', name: 'Status', search: false },
            { label: 'AccountsCount', name: 'AccountsCount', search: false },
            { label: 'DocumentsCount', name: 'DocumentsCount', search: false },
        ],
        rowNum: 5,
        rowList: [5, 10, 15],
        pager: '#pager',
        viewrecords: true,
        height: 250,
        caption: "Customer Records",
        search: {
            multipleSearch: true,
            multipleGroup: true,
            showQuery: true
        },
        gridComplete: function () {
            $("#grid").jqGrid('navGrid', '#pager', { edit: true, add: false, del: true, refresh: true},
                {
                    url: '/Customer/EditByJQGrid',
                    recreateForm: true,
                    closeAfterEdit: true,
                    beforeShowForm: function (form) {
                        // Disable the input field for the Id property
                        $('#Id', form).attr('readonly', 'readonly');
                    },
                    afterSubmit: function (response, postdata) {
                        var result = JSON.parse(response.responseText);
                        if (result.success) {
                            alert(result.message);
                            return [true];
                        } else {
                            alert(result.message);
                            return [false];
                        }
                    }
                },
                {
                    //for add thru jqgrid
                },
                {
                    url: '/Customer/Delete',
                    afterSubmit: function (response, postdata) {
                        var result = JSON.parse(response.responseText);
                        if (result.success) {
                            alert(result.message);
                            return [true];
                        } else {
                            alert(result.message);
                            return [false];
                        }
                    }
                },
                {
                    closeAfterSearch: true
                },
            );
        }
    });

    $('#btnTest').click(function () {
        window.location.href = '/Home/Add';
    });

    var $TransactionGrid = $("#TransactionGrid").jqGrid({
        url: '/Transaction/GetData',
        mtype: 'GET',
        datatype: 'json',
        contentType: "application/json; charset-utf-8",
        colNames: ['Id', 'TransactionType', 'Amount', 'Date', 'FromAccountNo', 'ToAccountNo'],
        colModel: [
            { label: 'Id', name: 'Id', key: true, search: true },
            { label: 'TransactionType', name: 'TransactionType', editable: true, search: true },
            { label: 'Amount', name: 'Amount', editable: true, search: true },
            { label: 'Date', name: 'Date', editable: true, search: true },
            { label: 'ToAccountNumber', name: 'ToAccountNumber', editable: true, search: true },
            { label: 'FromAccountNumber', name: 'FromAccountNumber', editable: true, search: true },
        ],
        rowNum: 5,
        rowList: [5, 10, 15],
        pager: '#TransactionPager',
        viewrecords: true,
        height: 250,
        caption: "Transaction Records",
        search: {
            multipleSearch: true,
            multipleGroup: true,
            showQuery: true
        },
        gridComplete: function ()
        {
            $("#TransactionGrid").jqGrid('navGrid', '#TransactionPager', { edit: false, add: false, del: false, refresh: true},
                {
                    url: '/Transaction/Edit',
                    recreateForm: true,
                    closeAfterEdit: true,
                    beforeShowForm: function (form) {
                        // Disable the input field for the Id property
                        $('#Id', form).attr('readonly', 'readonly');
                    },
                    afterSubmit: function (response, postdata) {
                        var result = JSON.parse(response.responseText);
                        if (result.success) {
                            alert(result.message);
                            return [true];
                        } else {
                            alert(result.message);
                            return [false];
                        }
                    },
                },
                {
                    closeAfterSearch: true
                },
            );
        }
    })

    var $AccountGrid = $("#AccountGrid").jqGrid({
        url: '/Account/GetData',
        mtype: 'GET',
        datatype: 'json',
        contentType: "application/json; charset-utf-8",
        colNames: ['Id', 'AccountNo', 'AccountType', 'Balance', 'Customer', 'TransactionsCount','Status'],
        colModel: [
            { label: 'Id', name: 'Id', key: true, search: true },
            { label: 'AccountNo', name: 'AccountNo', editable: true, search: true },
            { label: 'AccountTypeId', name: 'AccountTypeId', editable: true, search: true },
            { label: 'Balance', name: 'Balance', editable: true, search: true },
            { label: 'Customer', name: 'Customer', editable: true, search: true },
            { label: 'TransactionsCount', name: 'TransactionsCount', editable: true, search: true },
            { label: 'Status', name: 'Status', editable: true, search: true },
        ],
        rowNum: 5,
        rowList: [5, 10, 15],
        pager: '#AccountPager',
        viewrecords: true,
        height: 250,
        caption: "Account Records",
        search: {
            multipleSearch: true,
            multipleGroup: true,
            showQuery: true
        },
        gridComplete: function () {
            $("#AccountGrid").jqGrid('navGrid', '#AccountPager', { edit: false, add: false, del: false, refresh: true },
                {
                    url: '/Account/Edit',
                    recreateForm: true,
                    closeAfterEdit: true,
                    beforeShowForm: function (form) {
                        // Disable the input field for the Id property
                        $('#Id', form).attr('readonly', 'readonly');
                    },
                    afterSubmit: function (response, postdata) {
                        var result = JSON.parse(response.responseText);
                        if (result.success) {
                            alert(result.message);
                            return [true];
                        } else {
                            alert(result.message);
                            return [false];
                        }
                    },
                },
                {
                    closeAfterSearch: true
                },
            );
        }
    })

});
