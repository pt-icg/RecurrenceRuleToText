import { HttpClient } from './http-client-base.js';
export class RRuleHttpClient extends HttpClient {
    constructor() {
        super();
        this._initializeRequestInterceptor = () => {
            this.instance.interceptors.request.use(this._handleRequest, this._handleError);
        };
        this._handleRequest = (config) => {
            //config.headers['Authorization'] = 'Bearer ...';
            const antiForgeryToken = document.getElementsByName('__RequestVerificationToken')[0];
            config.headers = {
                'content-type': 'application/json;charset=UTF-8',
                'Accept': 'application/json',
                "RequestVerificationToken": antiForgeryToken.value
            };
            return config;
        };
        this.createRRule = (data) => this.instance.post('/Home/CreateRRule', data);
        this._initializeRequestInterceptor();
    }
}
//# sourceMappingURL=rrule-http-client.js.map