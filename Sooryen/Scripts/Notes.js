
function NoteViewModel() {

    //Make the self as 'this' reference
    var self = this;
    //Declare observable which will be bind with UI
    self.Id = ko.observable("");
    self.Title = ko.observable("");
    self.Body = ko.observable("");
   
    var Note = {
        Id: self.Id,
        Title: self.Title,
        Body: self.Body
    };

    self.Note = ko.observable();
    self.Notes = ko.observableArray(); // Contains the list of Notes

    // Initialize the view-model
    dataServie.AjaxCall('Get','/Note/GetAllNotes', {}, function (data) {
        self.Notes(data); //Put the response in ObservableArray
    }, function (err) {
        alert(err.status + " : " + err.statusText);
    });
 

   
    //Add New Item
    self.create = function () {
        if (Note.Title() != "") {
            dataServie.AjaxCall('Post','/Note/AddNote',  ko.toJSON(Note), function (data) {
                self.Notes.push(data);
                self.Title("");
                self.Body("");
            }, function (err) {
                alert(err);
            });

           
        }
        else {
            alert('Please Enter All the Values !!');
        }
    }
    // Delete Note details
    self.delete = function (Note) {
        if (confirm('Are you sure to Delete "' + Note.Ttile + '"  ??')) {
            var id = Note.Id;
            dataServie.AjaxCall('Post','/Note/DeleteNote/' + id, {}, function (data) {
                self.Notes.remove(Note);
            }, function (err) {
                alert(err);
            });

            
        }
    }

    // Edit Note details
    self.edit = function (Note) {
        self.Note(Note);
    }

    // Update Note details
    self.update = function () {
        var Note = self.Note();
        dataServie.AjaxCall('PUT','/Note/EditNote/' + id, {}, function (data) {
            self.Notes.removeAll();
            self.Note(data); //Put the response in ObservableArray
            self.Note(null);
            alert("Record Updated Successfully");
        }, function (err) {
            alert(err);
        });
       
    }

    // Reset Note details
    self.reset = function () {
        self.Title("");
        self.Body("");
    }

    // Cancel Note details
    self.cancel = function () {
        self.Note(null);
    }
}
var viewModel = new NoteViewModel();
ko.applyBindings(viewModel);


