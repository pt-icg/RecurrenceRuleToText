//https://levelup.gitconnected.com/enhance-your-http-request-with-axios-and-typescript-f52a6c6c2c8e
import { AxiosInstance, AxiosResponse, AxiosError } from "../lib/axios/index";

declare module "../lib/axios/index" {
    interface AxiosResponse<T = any> extends Promise<T> { }
}

declare var axios: any;

export abstract class HttpClient {
    protected readonly instance: AxiosInstance;

    public constructor() {
        this.instance = axios.create();

        this._initializeResponseInterceptor();
    }

    private _initializeResponseInterceptor = () => {
        this.instance.interceptors.response.use(
            this._handleResponse,
            this._handleError,
        );
    };

    private _handleResponse = ({ data }: AxiosResponse) => data;

    protected _handleError = (error: any) => Promise.reject(error);        
}