export interface IRetornoAPI<T> {
    message: string;
    data: T; 
    statusCode: string;
    status: string;
}