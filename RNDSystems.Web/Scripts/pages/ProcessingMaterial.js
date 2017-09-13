var columns = [

    {
        label: 'LPLotID',
        property: 'RNDLotID',
        sortable: true,
        width: '30px'
    },
    {
        label: 'HTLogID',
        property: 'HTLogID',
        sortable: true,
        width: '15px'
    },
     {
         label: 'Hole',
         property: 'Hole',
         sortable: true,
         width: '15px'
     },
     {
         label: 'Piece No.',
         property: 'PieceNo',
         sortable: true,
         width: '15px'
     },
     {
         label: 'SHTTemp',
         property: 'SHTTemp',
         sortable: true,
         width: '25px'
     },
     {
         label: 'SHSoakHrs',
         property: 'SHSoakHrs',
         sortable: true,
         width: '15px'
     },
    {
        label: 'SHSoakMns',
        property: 'SHSoakMns',
        sortable: true,
        width: '15px'
    },
    {
        label: 'SHTStartHrs',
        property: 'SHTStartHrs',
        sortable: true,
        width: '15px'
    },
    {
        label: 'SHTStartMns',
        property: 'SHTStartMns',
        sortable: true,
        width: '15px'
    },

     {
         label: 'SHTDate',
         property: 'SHTDate',
         sortable: true,
         width: '15px'
     },
     {
         label: 'StretchPct',
         property: 'StretchPct',
         sortable: true,
         width: '15px'
     },

   //  SDT(hrs) SDTHrs
   {
       label: 'SDT Hrs',
       property: 'AfterSHTHrs',
       sortable: true,
       width: '15px'
   },
     {
         label: 'SDT Mns',
         property: 'AfterSHTMns',
         sortable: true,
         width: '15px'
     },
     //NAT(Hrs)
    {
        label: 'NatAgingHrs',
        property: 'NatAgingHrs',
        sortable: true,
        width: '15px'
    },
    {
        label: 'NatAgingMns',
        property: 'NatAgingMns',
        sortable: true,
        width: '15px'
    },
    {
        label: 'AgeLotID',
        property: 'AgeLotID',
        sortable: true,
        width: '15px'
    },
    {
         label: 'Age Start Hrs',
         property: 'ArtStartHrs',
         sortable: true,
         width: '15px'
    }, 
    {
        label: 'Age Start Mins',
        property: 'ArtStartMns',
        sortable: true,
        width: '15px'
     },
    {
        label: 'Age Start Date',
        property: 'ArtAgeDate',
        sortable: true,
        width: '15px'
    },
    {
         label: 'Age Temp1',
         property: 'ArtAgeTemp1',
         sortable: true,
         width: '15px'
    },
    //Age Time-1
    {
        label: 'Age Hrs 1',
        property: 'ArtAgeHrs1',
        sortable: true,
        width: '15px'
    },
    {
        label: 'ArtAgeMns1',
        property: 'ArtAgeMns1',
        sortable: true,
        width: '15px'
    },
    {
        label: 'Age Temp2',
        property: 'ArtAgeTemp2',
        sortable: true,
        width: '15px'
    },
    //Age Time-2
    {
        label: 'ArtAgeHrs2',
        property: 'ArtAgeHrs2',
        sortable: true,
        width: '15px'
    },
    {
        label: 'ArtAgeMns2',
        property: 'ArtAgeMns2',
        sortable: true,
        width: '15px'
    },
    {
        label: 'ArtAgeTemp3',
        property: 'ArtAgeTemp3',
        sortable: true,
        width: '15px'
    },
     //Age Time-3
    {
        label: 'ArtAgeHrs3',
        property: 'ArtAgeHrs3',
        sortable: true,
        width: '15px'
    },
    {
        label: 'ArtAgeMns3',
        property: 'ArtAgeMns3',
        sortable: true,
        width: '15px'
    },
    {
        label: 'Target Count',
        property: 'TargetCount',
        sortable: true,
        width: '15px'
    },
    {
        label: 'Actual Count',
        property: 'ActualCount',
        sortable: true,
        width: '15px'
    },    
    {
        label: 'LP Temper',
        property: 'FinalTemper',
        sortable: true,
        width: '15px'
    },
    {
        label: 'Rec ID',
        property: 'RecID',
        sortable: true,
        width: '15px'
    },
     {
        label: 'Mill Lot No',
        property: 'MillLotNo',
        sortable: true,
        width: '30px'
    },


    //{
    //    label: 'SO num',
    //    property: 'Sonum',
    //    sortable: true,
    //    width: '15px'
    //},
    //{
    //    label: 'ProcessNo',
    //    property: 'ProcessNo',
    //    sortable: true,
    //    width: '25px'
    //},
    //{
    //    label: 'ProcessID',
    //    property: 'ProcessID',
    //    sortable: true,
    //    width: '15px'
    //},
    //{
    //    label: 'HTLogNo',
    //    property: 'HTLogNo',
    //    sortable: true,
    //    width: '15px'
    //},

    //{
    //    label: 'AgeLotNo',
    //    property: 'AgeLotNo',
    //    sortable: true,
    //    width: '15px'
    //},  
    {
        label: 'Edit',
        property: 'Edit',
        width: '10px'
    },
    {
        label: 'Delete',
        property: 'Delete',
        width: '10px'
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
        case 'RecID':
            // let's combine name and description into a single column
            customMarkup = '<div style="font-size:12px;">' + rowData.RecID + '</div>';
            break;
        case 'ArtAgeDate':
            // let's combine name and description into a single column
            customMarkup = rowData.ArtAgeDate;
            break;
        case 'SHTDate':
            // let's combine name and description into a single column
            customMarkup = rowData.SHTDate;
            break;
       case 'Edit':
            // let's combine name and description into a single column
            customMarkup = '<button onclick="GridEditClicked(' + rowData.RecID + ')" id="gridEdit" name="gridEdit" class="btn btn-info btn-sm center-block"><i class="fa fa-pencil"></i></button>';
            break;
        case 'Delete':
            // let's combine name and description into a single column
            customMarkup = '<button onclick="GridDeleteClicked(' + rowData.RecID + ')" id="gridDelete" name="gridDelete" class="btn btn-danger btn-sm center-block"><i class="fa fa-trash"></i></button>';
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
    item.attr('id', 'row' + helpers.rowData.RecID);
    callback();
}

// this example uses an API to fetch its datasource.
// the API handles filtering, sorting, searching, etc.
function customDataSource(options, callback) {
    // set options
    //debugger;
    var pageIndex = options.pageIndex;
    var pageSize = options.pageSize;
    var search = '';
    if ($('#searchWorkStudyNumber').val())
        search += ';' + 'WorkStudyID:' + $('#searchWorkStudyNumber').val();
    //if ($('#searchMillLotNo').val())
    //    search += ';' + 'MillLotNo:' + $('#searchMillLotNo').val();
    //if ($('#searchCustPartNo').val())
    //    search += ';' + 'CustPartNo:' + $('#searchCustPartNo').val();
    //if ($('#searchAlloy').val())
    //    search += ';' + 'AlloyTypes:' + $('#searchAlloy').val();
    //if ($('#searchTemper').val())
    //    search += ';' + 'TemperTypes:' + $('#searchTemper').val();
    //if ($('#Plant').val())
    //    search += ';' + 'Plant:' + $('#Plant').val();

    if ($('#searchMillLotNo').val())
        search += ';' + 'MillLotNo:' + $('#searchMillLotNo').val();
   
    if ($('#searchHTLogID').val())
        search += ';' + 'HTLogID:' + $('#searchHTLogID').val();
    if ($('#searchAgeLotID').val())
        search += ';' + 'AgeLotID:' + $('#searchAgeLotID').val();

    var options = {
        Screen: 'ProcessingMaterial',
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
    location.href = '/ProcessingMaterial/SaveProcessingMaterial/' + id;
}

/*
function AssignMaterial(ele) {
    var recId = $(ele).attr('data-RecId');
    var workStudyID = $(ele).attr('data-WorkStudyID');
    location.href = '/AssignMaterial/AssignMaterialList?recId=' + recId + '&workStudyID=' + workStudyID;
}
*/

function GridDeleteClicked(id) {
    $.ajax({
        url: Api + "api/Processing/" + id,
        headers: {
            Token: GetToken()
        },
        type: 'DELETE',
        //data: JSON.stringify(ApiViewModel),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $('#ProcessingMaterialRepeater').repeater('render');
        },
        error: function (x, y, z) {
        }
    });
}

$('#btnSearch').on('click', function () {
    $('#ProcessingMaterialRepeater').repeater('render');
});

$('#btnClear').on('click', function () {
    //$('#searchMillLotNo').val('');
    //$('#searchCustPartNo').val('');
    //$('#searchAlloy').val('');
    //$('#searchTemper').val('');
    $('#searchHTLogID').val('');
    $('#searchAgeLotID').val('');    
    $('#ProcessingMaterialRepeater').repeater('render');
});

$(document).ready(function () {
    if ($('#WorkStudyID').val() !== '0') {
        $('#searchWorkStudyNumber').prop("readonly", true);
    }
    /*
    $('#AlloyTypes').attr('data-live-search', 'true');
    $('#AlloyTypes').selectpicker();
    $('#TemperTypes').attr('data-live-search', 'true');
    $('#TemperTypes').selectpicker();
    */
    $('#btnAdd').on('click', function () {
        debugger;       
        location.href = '/ProcessingMaterial/SaveProcessingMaterial?id=0&workStudyId=' + $('#WorkStudyID').val();
        //location.href = '/ProcessingMaterial/ProcessingMaterial?id=0&workStudyId=' + $('#WorkStudyID').val();
    });
});