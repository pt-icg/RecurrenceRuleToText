export class HttpClient {
    constructor() {
        this._initializeResponseInterceptor = () => {
            this.instance.interceptors.response.use(this._handleResponse, this._handleError);
        };
        this._handleResponse = ({ data }) => data;
        this._handleError = (error) => Promise.reject(error);
        this.instance = axios.create();
        this._initializeResponseInterceptor();
    }
}
//# sourceMappingURL=http-client-base.js.map