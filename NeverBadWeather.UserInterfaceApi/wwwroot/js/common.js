function goTo(page) {
    const model = appContext.model;
    model.page = page;
    model.hasChanged();
}

function weatherTypeKeyFromIsRaining(isRaining) {
    return isRaining === null ? 'both' : isRaining ? 'rain' : 'noRain';
}