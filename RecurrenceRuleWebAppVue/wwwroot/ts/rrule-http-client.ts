//https://levelup.gitconnected.com/enhance-your-http-request-with-axios-and-typescript-f52a6c6c2c8e
//https://medium.com/@enetoOlveda/how-to-use-axios-typescript-like-a-pro-7c882f71e34a
import { AxiosRequestConfig } from "../lib/axios/index";
import { HttpClient } from './http-client-base.js';
//import { RRuleWrapperOld, RRuleResult, RecurrencePattern } from "./RRuleTypes";
import { RRuleWrapper, RRuleResult, RecurrencePattern } from "./RRuleTypes";

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

    public createRRule = (data) => this.instance.post<RRuleWrapper, RRuleResult>('/Home/CreateRRule', data);
    //public getRecurrencePattern = (data) => this.instance.get<string, RecurrencePattern>('/Home/GetRecurrencePattern', data);
    public getRecurrencePattern = (rrule: string) => this.instance.get<RecurrencePattern, RecurrencePattern>(`Home/GetRecurrencePattern?RRule=${encodeURI(rrule)}`);    
    //public getRecurrencePattern = (rrule: string) => this.instance.get<RecurrencePattern>(`Home/GetRecurrencePattern?RRule=${encodeURI(rrule)}`);   
}