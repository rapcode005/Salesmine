
function MypageLoad() {
    var vatdContent = document.getElementById("tdContent");
    var vatdMainMenu = document.getElementById("ctl00_MenuTr");
    var vatdSubMenu = document.getElementById("ctl00_MnuSubTab_ucProjectMenu");

    if (vatdMainMenu != null) 
    {    
        if (vatdSubMenu != null) 
        {    
            var tempHeight = vatdMainMenu.clientHeight + vatdSubMenu.clientHeight + 230;
            vatdContent.height = document.documentElement.clientHeight -tempHeight ;
        }
        else 
        {
            var tempHeight = vatdMainMenu.clientHeight + 177;
            vatdContent.height = document.documentElement.clientHeight -tempHeight ;
        }
    }
    else 
    {
        if(document.documentElement.clientHeight > 230)
        vatdContent.height = document.documentElement.clientHeight - 230;
    }   
}



function PopUp(ref, ExportTo) {
    var top = (screen.height / 2) - 225;
    var left = (screen.width / 2) - 325;
    var strFeatures = "toolbar=no,status=yes,menubar=no,location=middle";
    strFeatures = strFeatures + ",aq,scrollbars=yes,resizable=yes,height=450,width=650, top=";
    strFeatures = strFeatures + top + ", left=";
    strFeatures = strFeatures + left;

    newWin = window.open(ref, ExportTo, strFeatures);
    newWin.opener = top;
    newWin.focus();
}





  function IsContains(mainString,searchStr)
  {
    var result = mainString.indexOf(searchStr);
    if(result<0)
    {
      return false;
    }    
      return true;    
  }