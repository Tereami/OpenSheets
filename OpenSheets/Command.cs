﻿#region License
//Данный код опубликован под лицензией Creative Commons Attribution-NonCommercial-ShareAlike.
//Разрешено использовать, распространять, изменять и брать данный код за основу для производных в некоммерческих целях,
//при условии указания авторства и если производные лицензируются на тех же условиях.
//Код поставляется "как есть". Автор не несет ответственности за возможные последствия использования.
//Зуев Александр, 2019, все права защищены.
//This code is listed under the Creative Commons Attribution-NonCommercial-ShareAlike license.
//You may use, redistribute, remix, tweak, and build upon this work non-commercially, 
//as long as you credit the author by linking back and license your new creations under the same terms.
//This code is provided 'as is'. Author disclaims any implied warranty. 
//Zuev Aleksandr, 2019, all rigths reserved.
#endregion

#region Usings
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Collections.Generic;
#endregion

namespace OpenSheets
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;

            Selection sel = commandData.Application.ActiveUIDocument.Selection;
            if (sel.GetElementIds().Count < 2)
            {
                message = MyStrings.NoSelectedSheets;
                return Result.Failed;
            }

            List<ViewSheet> sheets = new List<ViewSheet>();
            foreach (ElementId id in sel.GetElementIds())
            {
                ViewSheet vs = doc.GetElement(id) as ViewSheet;
                if (vs == null) continue;

                commandData.Application.ActiveUIDocument.ActiveView = vs;
            }

            return Result.Succeeded;
        }
    }
}
