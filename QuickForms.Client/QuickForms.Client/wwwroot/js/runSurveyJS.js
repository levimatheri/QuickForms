window.runSurvey = async (dotNetHelper) => {
    await dotNetHelper.invokeMethodAsync('GetCurrentSurvey').then((surveyObj) => {
        var survey = new Survey.Model(JSON.parse(JSON.parse(surveyObj).Content));
        survey.render("surveyContainer");
    }).catch(error => {
        console.error(`ERROR! ${error}`);
    });
};