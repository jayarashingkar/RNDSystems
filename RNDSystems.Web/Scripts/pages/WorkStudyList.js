// define the columns in your datasource
var columns = [
    {
        label: 'R&D Study Number',
        property: 'WorkStudyID',
        sortable: true,
        width: '50px'
    },
    {
        label: 'Study Title',
        property: 'StudyTitle',
        sortable: true,
        width: '50px'
    },
    {
        label: 'Study Type',
        property: 'StudyTypeDesc',
        sortable: true
    },
    {
        label: 'Location',
        property: 'PlantDesc',
        sortable: true
    },
    {
        label: 'Planned OutSide Cost',
        property: 'PlanOSCost',
        sortable: true
    },
    {
        label: 'Actual OutSide Cost',
        property: 'AcctOSCost',
        sortable: true
    },
    {
        label: 'Status',
        property: 'StudyStatusDesc',
        sortable: true
    },
    {
        label: 'Started Date',
        property: 'StartDate',
        width: '50px',
        sortable: true

    },
    {
        label: 'Due Date',
        property: 'DueDate',
        width: '50px',
        sortable: true

    },
    {
        label: 'Complete Date',
        property: 'CompleteDate',
        width: '50px',
        sortable: true

    },
    {
        label: 'Assign Material',
        property: 'AssignMaterial',
        width: '50px',

    },
    {
        label: 'Material Processing',
        property: 'MaterialProcessing',
        width: '50px',

    },
    {
        label: 'Material Testing',
        property: 'MaterialTesting',
        width: '50px',

    },
    {
        label: 'Edit',
        property: 'Edit',
        width: '50px',

    },
    {
        label: 'Delete',
        property: 'Delete',
        width: '50px',
    }
];

function customColumnRenderer(helpers, callback) {
    // determine what column is being rendered
    var column = helpers.columnAttr;

    // get all the data for the entire row
    var rowData = helpers.rowData;
    var customMarkup = '';

    // only override the output for specific columns.
    // will default to output the text value of the row item
    switch (column) {
        case 'RecId':
            // let's combine name and description into a single column
            customMarkup = '<div style="font-size:12px;">' + rowData.RecId + '</div>';
            break;
        case 'AssignMaterial':
            // let's combine name and description into a single column
            //customMarkup = '<button onclick="AssignMaterial(' + rowData.RecId + ')" id="gridAM" name="gridAM" class="btn btn-warning btn-sm center-block"><i class="fa fa-book"></i></button>';
            customMarkup = "<button id='gridAM' data-RecId='" + rowData.RecId + "' data-WorkStudyID='" + rowData.WorkStudyID + "'  onclick= 'AssignMaterial(this)' name= 'gridAM' class='btn btn-warning btn-sm center-block' > <i class='fa fa-book'></i></button > ";
            break;
        case 'MaterialProcessing':
             //let's combine name and description into a single column
            customMarkup = "<button id='gridPM' data-RecId='" + rowData.RecId + "' data-WorkStudyID='" + rowData.WorkStudyID + "'  onclick= 'ProcessingMaterial(this)' name= 'gridPM' class='btn btn-primary btn-sm center-block' > <i class='fa fa-book'></i></button > ";
            break;
        case 'MaterialTesting':
            // let's combine name and description into a single column
            customMarkup = '<button onclick="GridEditClicked(' + rowData.RecId + ')" id="gridEdit" name="gridEdit" class="btn btn-success btn-sm center-block"><i class="fa fa-book"></i></button>';
            break;
        case 'Edit':
            // let's combine name and description into a single column
            customMarkup = '<button onclick="GridEditClicked(' + rowData.RecId + ')" id="gridEdit" name="gridEdit" class="btn btn-info btn-sm center-block"><i class="fa fa-pencil"></i></button>';
            break;
        case 'Delete':
            // let's combine name and description into a single column
            customMarkup = '<button onclick="GridDeleteClicked(' + rowData.RecId + ')" id="gridDelete" name="gridDelete" class="btn btn-danger btn-sm center-block"><i class="fa fa-trash"></i></button>';
            break;
        default:
            // otherwise, just use the existing text value
            customMarkup = helpers.item.text();
            break;
    }

    helpers.item.html(customMarkup);

    callback();
}

function customRowRenderer(helpers, callback) {
    // let's get the id and add it to the "tr" DOM element
    var item = helpers.item;
    item.attr('id', 'row' + helpers.rowData.RecId);

    callback();
}

// this example uses an API to fetch its datasource.
// the API handles filtering, sorting, searching, etc.
function customDataSource(options, callback) {
    // set options    
    var pageIndex = options.pageIndex;
    var pageSize = options.pageSize;
    var search = '';
    if ($('#searchWorkStudyNumber').val())
        search += ';' + 'WorkStudyID:' + $('#searchWorkStudyNumber').val();
    if ($('#StudyType').val())
        search += ';' + 'StudyType:' + $('#StudyType').val();
    if ($('#Plant').val())
        search += ';' + 'Plant:' + $('#Plant').val();
    if ($('#StudyStatus').val() && $('#StudyStatus').val() !== '-1')
        search += ';' + 'StudyStatus:' + $('#StudyStatus').val();
    if ($('#searchFromDate').val()) {
        var searchFromDate = $('#searchFromDate').datepicker();
        searchFromDate = $("#searchFromDate").data('datepicker').getFormattedDate('yyyy-mm-dd');
        if (searchFromDate && searchFromDate !== '')
            search += ';' + 'searchFromDate:' + searchFromDate;
    }
    if ($('#searchToDate').val()) {
        var searchToDate = $('#searchToDate').datepicker();
        searchToDate = $("#searchToDate").data('datepicker').getFormattedDate('yyyy-mm-dd');
        if (searchToDate && searchToDate !== '')
            search += ';' + 'searchToDate:' + searchToDate;
    }

    var options = {
        Screen: 'WorkStudy',
        pageIndex: pageIndex,
        pageSize: pageSize,
        sortDirection: options.sortDirection,
        sortBy: options.sortProperty,
        filterBy: options.filter.value || '',
        searchBy: search || ''
    };
    // call API, posting options
    $.ajax({
        type: 'post',
        url: Api + 'api/grid',
        headers: {
            Token: GetToken()
        },
        data: options
    })
        .done(function (data) {
            var items = data.items;
            var totalItems = data.total;
            var totalPages = Math.ceil(totalItems / pageSize);
            var startIndex = (pageIndex * pageSize) + 1;
            var endIndex = (startIndex + pageSize) - 1;

            if (items) {
                if (endIndex > items.length) {
                    endIndex = items.length;
                }
            }
            // configure datasource
            var dataSource = {
                page: pageIndex,
                pages: totalPages,
                count: totalItems,
                start: startIndex,
                end: endIndex,
                columns: columns,
                items: items
            };

            // invoke callback to render repeater
            callback(dataSource);
        });
}

function GridEditClicked(id) {
    location.href = '/WorkStudy/SaveWorkStudy/' + id;
}

function AssignMaterial(ele) {
    var recId = $(ele).attr('data-RecId');
    var workStudyID = $(ele).attr('data-WorkStudyID');
    location.href = '/AssignMaterial/AssignMaterialList?recId=' + recId + '&workStudyID=' + workStudyID;
}
function ProcessingMaterial(ele) {
    var recId = $(ele).attr('data-RecId');
    var workStudyID = $(ele).attr('data-WorkStudyID');
    location.href = '/ProcessingMaterial/ProcessingMaterialList?recId=' + recId + '&workStudyID=' + workStudyID;
}

function GridDeleteClicked(id) {
    $.ajax({
        url: Api + "api/workstudy/" + id,
        headers: {
            Token: GetToken()
        },
        type: 'DELETE',
        //data: JSON.stringify(ApiViewModel),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $('#WorkStudyRepeater').repeater('render');
        },
        error: function (x, y, z) {
        }
    });
}

$('#btnSearch').on('click', function () {
    $('#WorkStudyRepeater').repeater('render');
});

$('#btnClear').on('click', function () {
    //check if this should be dopbox - currently keep text for search
    $('#searchWorkStudyNumber').val('');
    $('#StudyType').selectpicker('val', "-1");
    $('#Plant').selectpicker('val', "-1");
    $('#StudyStatus').selectpicker('val', "-1")
    $('#searchFromDate').val('');
    $('#searchToDate').val('');

    $('#WorkStudyRepeater').repeater('render');
});

$(document).ready(function () {
    $('#StudyStatus').attr('data-live-search', 'true');
    $('#StudyStatus').selectpicker();
    $('#StudyType').attr('data-live-search', 'true');
    $('#StudyType').selectpicker();
    $('#Plant').attr('data-live-search', 'true');
    $('#Plant').selectpicker();
    $('#searchFromDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });
    $('#searchToDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });
    $('#btnAdd').on('click', function () {
        console.log('cliked');
        location.href = '/WorkStudy/SaveWorkStudy/0'
    });
});