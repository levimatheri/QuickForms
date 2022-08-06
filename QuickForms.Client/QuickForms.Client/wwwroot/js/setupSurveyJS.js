const creatorOptions = {
    showLogicTab: true,
    //isAutoSave: true
};
const creator = new SurveyCreator.SurveyCreator(creatorOptions);

window.setupSurveyJS = async (dotNetHelper) => {
    var currentSurvey;

    await dotNetHelper
        .invokeMethodAsync('GetCurrentSurvey', currentSurvey).then((surveyObj) => {
            currentSurvey = surveyObj;
            creator.text = currentSurvey.content;
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