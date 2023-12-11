Imports SBUtility.Utility

Public Interface IGeneral

    ''This will set the images of the buttons at runtime
    Sub SetButtonImages()

    ''Here will will use this function to fill-up all Combos and Listboxes on the form
    ''Optional condition would be used to fill-up combo or Listbox; which based on the selection of some other combo.
    Sub FillCombos(Optional ByVal Condition As String = "")

    ''Here we will use this procedure to load all master records; respective to the screen.
    Sub GetAllRecords(Optional ByVal Condition As String = "")

    
    ''This procedure will be used to set the formatting of the grid on that form. For Example, Grid's columns show/Hide,
    '' Caption setting, columns' width etc.
    Sub ApplyGridSettings(Optional ByVal Condition As String = "")

    ''This procedure will be used (if applicable) to set Active/Deactive or Visible/Invisible some controls on form,
    ''which are based on System level configuration
    Sub SetConfigurationBaseSetting()

    ''This procedure will be used to set the navigation buttons as per Mode
    Sub SetNavigationButtons(ByVal Mode As EnumDataMode, Optional ByVal Condition As String = "")

    ''here we will clear all the contols of the screen for New Mode
    Sub ReSetControls(Optional ByVal Condition As String = "")

    ''Here we will pass an argument MODE (New|Edit|Disabled), which will be overwritten according to the rights 
    ''available to user on that screen
    Sub ApplySecurity(ByVal Mode As EnumDataMode, Optional ByVal Condition As String = "")

    ''Here we will apply Front End Validations.
    Function IsValidate(Optional ByVal Mode As EnumDataMode = EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean

    ''Here we will create an instance of the class, according to the form, and will set the properties of the object
    ''Later this object will be refered in Save|Update|Delete function.
    Sub FillModel(Optional ByVal Condition As String = "")

    ''Here we will call DAL Function for SAVE, and if the function successfully Saves the records
    ''then the function will return True, otherwise returns False
    Function Save(Optional ByVal Condition As String = "") As Boolean

    ''Here we will call DAL Function for Update the selected record, and if the function successfully Updates the records
    ''then the function will return True, otherwise returns False
    Function Update(Optional ByVal Condition As String = "") As Boolean

    ''Here we will call DAL Function for Delete the selected record, and if the function successfully Deletes the records
    ''then the function will return True, otherwise returns False
    Function Delete(Optional ByVal Condition As String = "") As Boolean








End Interface
