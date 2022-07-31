(function (global) {
    const creatorOptions = {
        showLogicTab: true,
        //isAutoSave: true
    };
    const creator = new SurveyCreator.SurveyCreator(creatorOptions);

    global.setupSurveyJS = async (dotNetHelper) => {
        var currentSurvey;

        await dotNetHelper
            .invokeMethodAsync('GetCurrentSurvey', currentSurvey).then((jsonData) => {
                currentSurvey = JSON.parse(jsonData);
                creator.text = currentSurvey.Content;
                creator.render("surveyCreator");
            }).catch(error => {
                console.error(`ERROR! ${error}`);
            });

        creator.saveSurveyFunc = async (saveNo, callback) => {
            currentSurvey.Content = JSON.stringify(creator.JSON);
            console.log(currentSurvey);
            await dotNetHelper
                .invokeMethodAsync('UpdateCurrentSurvey', currentSurvey).then(() => {
                    // success. resolve callback
                    callback(saveNo, true);
                }).catch(error => {
                    console.error(`ERROR! ${error}`);
                    callback(saveNo, false);
                });

            // saveSurveyJson(
            //     "https://your-web-service.com/",
            //     creator.JSON,
            //     saveNo,
            //     callback
            // );
        };
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