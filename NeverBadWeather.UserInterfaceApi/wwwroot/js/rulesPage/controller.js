async function updateRule() {
    const ruleEdit = appContext.model.inputs.ruleEdit;
    const isBoth = ruleEdit.weatherType === null || ruleEdit.weatherType === 'both';
    const rule = {
        id: ruleEdit.id || null,
        isRaining: isBoth ? null : ruleEdit.weatherType === 'rain',
        fromTemperature: ruleEdit.temperature.from,
        toTemperature: ruleEdit.temperature.to,
        clothes: ruleEdit.clothes,
    };
    await appContext.api.createOrUpdateRule(rule);
    await appContext.api.loadRules();
    appContext.model.hasChanged();
}

async function deleteRule(index) {
    const model = appContext.model;
    const rule = model.rules[index];
    await appContext.api.deleteRule(rule);
    await appContext.api.loadRules();
    appContext.model.hasChanged();
}

function selectRule(index) {
    const model = appContext.model;
    const rule = model.rules[index];
    const weatherType = weatherTypeKeyFromIsRaining(rule.isRaining);
    model.inputs.ruleEdit = {
        id: rule.id,
        temperature: {
            from: rule.fromTemperature,
            to: rule.toTemperature,
        },
        weatherType: weatherType,
        clothes: rule.clothes,
    };
    model.hasChanged();
}

function createRule() {
    const model = appContext.model;
    model.inputs.ruleEdit = {
        id: null,
        temperature: {
            from: 10,
            to: 20,
        },
        weatherType: null,
        clothes: '',
    };
    model.hasChanged();
}

function changeTemperature(temperatureField, change) {
    const model = appContext.model;
    const temperature = model.inputs.ruleEdit.temperature;
    temperature[temperatureField] = temperature[temperatureField] + change;
    model.hasChanged();
}