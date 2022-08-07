var surveyId;
window.runSurvey = async (dotNetHelper) => {
    await dotNetHelper.invokeMethodAsync('GetCurrentSurvey').then((surveyObj) => {
        surveyId = JSON.parse(surveyObj).Id;
        var survey = new Survey.Model(JSON.parse(surveyObj).Content);
        survey.render("surveyContainer");

        async function surveyComplete(sender) {
            var surveyResult = {
                surveyId: surveyId,
                surveyResult: JSON.stringify(sender.data)
            }

            await dotNetHelper
                .invokeMethodAsync('SaveCurrentSurveyResults', surveyResult).then(() => {
                    // success. resolve callback
                }).catch(error => {
                    console.error(`ERROR! ${error}`);
                });
        }

        survey.onComplete.add(surveyComplete);
    }).catch(error => {
        console.error(`ERROR! ${error}`);
    });
};