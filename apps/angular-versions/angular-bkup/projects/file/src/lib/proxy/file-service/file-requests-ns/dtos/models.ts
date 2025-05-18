
export interface FileRequestCompleteInputDto {
  token?: string;
  traktTypeId: number;
}

export interface FileRequestStartDto {
  fileRequestId?: string;
  returnUrl: string;
  cancelUrl?: string;
}

export interface FileRequestStartResultDto {
  checkoutLink?: string;
}
