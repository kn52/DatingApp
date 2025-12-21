export interface ApiResponse<T> {
  Success: boolean;
  Message: string;
  StatusCode: number;
  Data: T;
}

export class ApiResponseUtil {
  static success<T>(data: T, message = 'Success'): ApiResponse<T> {
    return {
      Success: true,
      Message: message,
      StatusCode: 200,
      Data: data
    };
  }

  static error<T>(message: string, data: T): ApiResponse<T> {
    return {
      Success: false,
      Message: message,
      StatusCode: 500,
      Data: data
    };
  }
}
