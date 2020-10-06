//https://levelup.gitconnected.com/enhance-your-http-request-with-axios-and-typescript-f52a6c6c2c8e
import { AxiosRequestConfig } from "../lib/axios/index";
import { HttpClient } from './http-client-base.js';
import { RRuleWrapperOld, RRuleResult } from "./RRuleTypes";

export class RRuleHttpClient extends HttpClient {
    protected readonly config: AxiosRequestConfig;

    public constructor() {
        super()
        this._initializeRequestInterceptor();
    }

    private _initializeRequestInterceptor = () => {
        this.instance.interceptors.request.use(
            this._handleRequest,
            this._handleError,
        );
    };

    private _handleRequest = (config: AxiosRequestConfig) => {
        //config.headers['Authorization'] = 'Bearer ...';
        const antiForgeryToken: any = document.getElementsByName('__RequestVerificationToken')[0];
        config.headers = {
            'content-type': 'application/json;charset=UTF-8',
            'Accept': 'application/json',
            "RequestVerificationToken": antiForgeryToken.value
        }        
        return config;
    };

    public createRRule = (data) => this.instance.post<RRuleWrapperOld, RRuleResult>('/Home/CreateRRule', data);

}