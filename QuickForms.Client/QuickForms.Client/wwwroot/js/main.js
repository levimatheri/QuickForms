(function (global) {
    const creatorOptions = {
        showLogicTab: true,
        isAutoSave: true
    };
    const creator = new SurveyCreator.SurveyCreator(creatorOptions);

    global.setupSurveyJS = (dotNetHelper) => {
        let getSurveyPromise = new Promise((resolve, reject) => {
            var jsonData = dotNetHelper.invokeMethodAsync('GetCurrentSurvey');
            if (jsonData) {
                resolve(jsonData);
            }
        });

        getSurveyPromise.then((jsonData) => {
            var surveyData = JSON.parse(jsonData);
            creator.text = window.localStorage.getItem("survey-json") || JSON.stringify(surveyData.Content);

            creator.render("surveyCreator");
        });
        
        
      //  console.log("Here!");
        //const defaultJson = {
        //    pages: [{
        //        name: "Name",
        //        elements: [{
        //            name: "FirstName",
        //            title: "Enter your first name:",
        //            type: "text"
        //        }, {
        //            name: "LastName",
        //            title: "Enter your last name:",
        //            type: "text"
        //        }]
        //    }]
        //};
        
        //creator.saveSurveyFunc = (saveNo, callback) => {
        //    window.localStorage.setItem("survey-json", creator.text);
        //    callback(saveNo, true);
        //    // saveSurveyJson(
        //    //     "https://your-web-service.com/",
        //    //     creator.JSON,
        //    //     saveNo,
        //    //     callback
        //    // );
        //};

       // console.log("Rendering survey creator");
        

        //document.addEventListener("DOMContentLoaded", function () {
        //    console.log("Rendering survey creator");
        //    creator.render("surveyCreator");
        //});

      //  global.creator = creator;
    };


    // function saveSurveyJson(url, json, saveNo, callback) {
    //     const request = new XMLHttpRequest();
    //     request.open('POST', url);
    //     request.setRequestHeader('Content-Type', 'application/json;charset=UTF-8');
    //     request.addEventListener('load', () => {
    //         callback(saveNo, true);
    //     });
    //     request.addEventListener('error', () => {
    //         callback(saveNo, false);
    //     });
    //     request.send(JSON.stringify(json));
    // }
})(window);