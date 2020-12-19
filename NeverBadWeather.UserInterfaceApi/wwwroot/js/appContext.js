const appContext = {};

(async function () {
    const now = new Date();
    const hours = now.getHours();

    appContext.model = {
        page: 'main',
        inputs: {
            weatherRecommendation: {
                time: {
                    date: now.toISOString().substr(0, 10),
                    from: hours + 1,
                    to: 20,
                },
            },
            ruleEdit: {
                id: null,
                temperature: {
                    from: 10,
                    to: 20,
                },
                weatherType: null,
                clothes: null,
            },
        },
        recommendation: null,
        weatherTypes: {
            both: 'både regn og oppholdsvær',
            rain: 'regn',
            noRain: 'oppholdsvær',
        },
        hasChanged() {
            if (this.callback) {
                this.callback();
            }
        },
        subscribe(callback) {
            this.callback = callback;
        }
    };
    appContext.view = {
        updateFunctions: {},
        add(pageName, updateFunction) {
            this.updateFunctions[pageName] = updateFunction;
            if (appContext.model.page === pageName) {
                updateFunction();
            }
        },
        update() {
            const page = appContext.model.page;
            const updateFunction = appContext.view.updateFunctions[page];
            updateFunction();
        }
    };
    appContext.api = {
        async loadRules() {
            const result = await axios.get('/api/clothingRule');
            appContext.model.rules = result.data;
        },
        async createOrUpdateRule(rule) {
            const isSuccess = await axios.post('/api/clothingRule', rule);
            return isSuccess;
        },
        async deleteRule(rule) {
            const isSuccess = await axios.put('/api/clothingRule', rule);
            return isSuccess;
        },
        async getClothingRecommendation(request) {
            const response = await axios.post('/api/clothingRecommendation', request);
            return response.data;
        },
    }
    appContext.model.subscribe(appContext.view.update);
    appContext.api.loadRules();
    navigator.geolocation.getCurrentPosition(function (position) { appContext.model.position=position});

})();

