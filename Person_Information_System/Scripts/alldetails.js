

const pqOptions = {
    width: "auto",
    height: 250,
    showTitle: false,
    showHeader: true,
    showTop: true,
    showToolbar: false,
    showBottom: false,
    wrap: true,
    hwrap: false,
    sortable: false,
    editable: false,
    resizable: false,
    collapsible: false,
    draggable: true, dragColumns: { enabled: true },
    scrollModel: { autoFit: true },
    numberCell: { show: true, resizable: true, title: "S.N.", minWidth: 30 },
    pageModel: { curPage: 1, rPP: 10, type: "local" },
    columnTemplate: { wrap: true, editable: false, dataType: "string", halign: "center", hvalign: "center", resizable: true, styleHead: { 'font-weight': "bold" } },
};

// function DemoItem(id, name) {
//     var self = this;

//     self.id = ko.observable(id);
//     self.Name = ko.observable(name);
//     self.Selected = ko.observable(false);
// }

function Personal() {
    const self = this;

    const models = {
        MyModel: function (item) {
            item = item || {};
      
        },

      

        UiElements: function () {
            self.MyModel = ko.observable(new models.MyModel());
            self.DataListAllDetails = ko.observableArray([]);

        },
    };

    const UiEvents = {
     

        functions: {
            SelectAjaxCall: function () {

                $.ajax({

                    type: "POST",
                    url: '/Home/GetAllDetails',
                    dataType: "json",
                   /* data: JSON.stringify({ "data": PersonInfo }),*/
                    contentType: "application/json; charset=utf-8",
                    success: function (result) {
                        debugger;
                        self.DataListAllDetails([]);
                        self.DataListAllDetails(result.Data);
                        UiEvents.functions.DetailSave("dataGrid1");

                        alert("Success");
                        //alert(data.Name + "-" + data.Email + "-" + data.Phone + "-" + data.Age);

                    }

                });
            },

            DeleteAjaxCall: function (PersonId) {

                $.ajax({

                    type: "POST",
                    url: '/Home/DeleteDetails',
                  
                    dataType: "json",
                    data: JSON.stringify({ "Id": PersonId }),
                    contentType: "application/json; charset=utf-8",
                    success: function (result) {
                   /*     debugger;*/
                     

                        alert("Data DELETED");
                        //alert(data.Name + "-" + data.Email + "-" + data.Phone + "-" + data.Age);

                    }

                });
            },

            EditAjaxCall: function (PersonId) {

                $.ajax({

                    type: "POST",
                    url: '/Home/EditDetails',

                    dataType: "json",
                    data: JSON.stringify({ "Id": PersonId }),
                    contentType: "application/json; charset=utf-8",
                    success: function (result) {
                        /*     debugger;*/


                        alert("Data EDITED");
                        //alert(data.Name + "-" + data.Email + "-" + data.Phone + "-" + data.Age);

                    }

                });
            },

    //        Update: function () {
    //            var res = validate();
    //          if (res == false) {
    //        return false;
    //            }
    //            let PersonInfo = {

    //                Salutation: self.MyModel().SalutationName(),
    //                FirstName: self.MyModel().FirstName(),
    //                LastName: self.MyModel().LastName(),
    //                Age: self.MyModel().Age(),
    //                Email: self.MyModel().Email(),
    //                Phone: self.MyModel().Phone(),
    //                Gender: self.MyModel().SelectedGender(),
    //                Educationlist: self.Edulist(),
    //                nationality: self.MyModel().nationality(),
    //                AddressList: ko.toJS(obj.DataList())

    //            }
    //            console.log(PersonInfo);

    //         var Personobj = {
    //             PersonId: $('#PersonId').val(),
    //             FirstName: $('#FirstName').val(),
    //             LastName: $('#LastName').val(),
    //             Email: $('#Email').val(),
    //             Phone: $('#PhoneNumber').val(),
    //             Age: $('#Age').val(),
    //             nationality: $('#Nationality').val(),

    //};
    //       $.ajax({
    //    url: "/Home/EditDetails",
    //    data: JSON.stringify(Personobj),
    //    type: "POST",
    //    contentType: "application/json;charset=utf-8",
    //    dataType: "json",
    //           success: function (PersonInfo) {
    //        loadData();
    //        $('#myModal').modal('hide');
    //        $('#PersonId').val("");
    //        $('#FirstName').val("");
    //        $('#LastName').val("");
    //        $('#Email').val("");
    //        $('#PhoneNumber').val
    //        $('#Age').val("");
    //        $('#Nationality').val("");
    //    },
    //    error: function (errormessage) {
    //        alert(errormessage.responseText);
    //    }
    //});
    //      }  ,

            DetailSave: function (control) {
                if ($("#" + control).pqGrid("instance")) {
                    // $("#" + control).pqGrid("destroy");
                    $("#" + control).pqGrid('option', 'dataModel.data', ko.toJS(self.DataListAllDetails()));
                    $("#" + control).pqGrid('refreshDataAndView');
                }
                else {
                    const options = Object.assign({}, pqOptions);
                    options.colModel = [
                      

                        { title: "Salutation", align: "left", dataIndx: "Salutation", width: "10%" },
                        { title: "FirstName", align: "center", dataIndx: "FirstName", width: "10%" },
                        { title: "LastName", align: "center", dataIndx: "LastName", width: "10%" },
                        { title: "Email", align: "Center", dataIndx: "Email", width: "10%" },
                        { title: "PhoneNumber", align: "Center", dataIndx: "Phone", width: "10%" },
                        { title: "Age", align: "Center", dataIndx: "Age", width: "10%" },
                        { title: "Gender", align: "Center", dataIndx: "Gender", width: "10%" },
                        { title: "Nationality", align: "Center", dataIndx: "nationality", width: "10%" },


                        {
                            title: "Action", align: "Center", width: "20%", render: function (ui) {

                                return `<button class="btn btn-danger" onclick="obj.delete(${ui.rowData.PersonId});" type="button"><i class="fas fa-trash fa-lg">  Delete</i></button>  <button class="btn" style="background-color: #66CD00" onclick="obj.edit(${ui.rowData.PersonId});" type="button"><i class="fas fa-edit fa-lg">Edit</i></button>`;
                            }
                        },

                    ];

                    options.dataModel = { data: ko.toJS(self.DataListAllDetails()) };
                    options.showBottom = false;
                    $("#" + control).pqGrid(options);
                }
            }

        },


    };

    self.delete = function (PersonId) {
        UiEvents.functions.DeleteAjaxCall(PersonId);
     
    };
    self.edit = function (PersonId) {
        UiEvents.functions.EditAjaxCall(PersonId)
        UiEvents.functions.Update()
    }

    function Init() {
        models.UiElements();
        //UiEvents.functions.DetailSave("dataGrid1");
        UiEvents.functions.SelectAjaxCall();

       

    }
    Init();

}

var obj;

$(document).ready(function () {
    obj = new Personal();
    ko.applyBindings(obj);


});


