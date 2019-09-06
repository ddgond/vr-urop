"""Generated client library for iamcredentials version v1."""
# NOTE: This file is autogenerated and should not be edited by hand.
from apitools.base.py import base_api
from googlecloudsdk.third_party.apis.iamcredentials.v1 import iamcredentials_v1_messages as messages


class IamcredentialsV1(base_api.BaseApiClient):
  """Generated client library for service iamcredentials version v1."""

  MESSAGES_MODULE = messages
  BASE_URL = u'https://iamcredentials.googleapis.com/'

  _PACKAGE = u'iamcredentials'
  _SCOPES = [u'https://www.googleapis.com/auth/cloud-platform']
  _VERSION = u'v1'
  _CLIENT_ID = '1042881264118.apps.googleusercontent.com'
  _CLIENT_SECRET = 'x_Tw5K8nnjoRAqULM9PFAC2b'
  _USER_AGENT = 'x_Tw5K8nnjoRAqULM9PFAC2b'
  _CLIENT_CLASS_NAME = u'IamcredentialsV1'
  _URL_VERSION = u'v1'
  _API_KEY = None

  def __init__(self, url='', credentials=None,
               get_credentials=True, http=None, model=None,
               log_request=False, log_response=False,
               credentials_args=None, default_global_params=None,
               additional_http_headers=None, response_encoding=None):
    """Create a new iamcredentials handle."""
    url = url or self.BASE_URL
    super(IamcredentialsV1, self).__init__(
        url, credentials=credentials,
        get_credentials=get_credentials, http=http, model=model,
        log_request=log_request, log_response=log_response,
        credentials_args=credentials_args,
        default_global_params=default_global_params,
        additional_http_headers=additional_http_headers,
        response_encoding=response_encoding)
    self.projects_serviceAccounts = self.ProjectsServiceAccountsService(self)
    self.projects = self.ProjectsService(self)

  class ProjectsServiceAccountsService(base_api.BaseApiService):
    """Service class for the projects_serviceAccounts resource."""

    _NAME = u'projects_serviceAccounts'

    def __init__(self, client):
      super(IamcredentialsV1.ProjectsServiceAccountsService, self).__init__(client)
      self._upload_configs = {
          }

    def GenerateAccessToken(self, request, global_params=None):
      r"""Generates an OAuth 2.0 access token for a service account.

      Args:
        request: (IamcredentialsProjectsServiceAccountsGenerateAccessTokenRequest) input message
        global_params: (StandardQueryParameters, default: None) global arguments
      Returns:
        (GenerateAccessTokenResponse) The response message.
      """
      config = self.GetMethodConfig('GenerateAccessToken')
      return self._RunMethod(
          config, request, global_params=global_params)

    GenerateAccessToken.method_config = lambda: base_api.ApiMethodInfo(
        flat_path=u'v1/projects/{projectsId}/serviceAccounts/{serviceAccountsId}:generateAccessToken',
        http_method=u'POST',
        method_id=u'iamcredentials.projects.serviceAccounts.generateAccessToken',
        ordered_params=[u'name'],
        path_params=[u'name'],
        query_params=[],
        relative_path=u'v1/{+name}:generateAccessToken',
        request_field=u'generateAccessTokenRequest',
        request_type_name=u'IamcredentialsProjectsServiceAccountsGenerateAccessTokenRequest',
        response_type_name=u'GenerateAccessTokenResponse',
        supports_download=False,
    )

    def GenerateIdToken(self, request, global_params=None):
      r"""Generates an OpenID Connect ID token for a service account.

      Args:
        request: (IamcredentialsProjectsServiceAccountsGenerateIdTokenRequest) input message
        global_params: (StandardQueryParameters, default: None) global arguments
      Returns:
        (GenerateIdTokenResponse) The response message.
      """
      config = self.GetMethodConfig('GenerateIdToken')
      return self._RunMethod(
          config, request, global_params=global_params)

    GenerateIdToken.method_config = lambda: base_api.ApiMethodInfo(
        flat_path=u'v1/projects/{projectsId}/serviceAccounts/{serviceAccountsId}:generateIdToken',
        http_method=u'POST',
        method_id=u'iamcredentials.projects.serviceAccounts.generateIdToken',
        ordered_params=[u'name'],
        path_params=[u'name'],
        query_params=[],
        relative_path=u'v1/{+name}:generateIdToken',
        request_field=u'generateIdTokenRequest',
        request_type_name=u'IamcredentialsProjectsServiceAccountsGenerateIdTokenRequest',
        response_type_name=u'GenerateIdTokenResponse',
        supports_download=False,
    )

    def GenerateIdentityBindingAccessToken(self, request, global_params=None):
      r"""GenerateIdentityBindingAccessToken method for the projects_serviceAccounts service.

      Args:
        request: (IamcredentialsProjectsServiceAccountsGenerateIdentityBindingAccessTokenRequest) input message
        global_params: (StandardQueryParameters, default: None) global arguments
      Returns:
        (GenerateIdentityBindingAccessTokenResponse) The response message.
      """
      config = self.GetMethodConfig('GenerateIdentityBindingAccessToken')
      return self._RunMethod(
          config, request, global_params=global_params)

    GenerateIdentityBindingAccessToken.method_config = lambda: base_api.ApiMethodInfo(
        flat_path=u'v1/projects/{projectsId}/serviceAccounts/{serviceAccountsId}:generateIdentityBindingAccessToken',
        http_method=u'POST',
        method_id=u'iamcredentials.projects.serviceAccounts.generateIdentityBindingAccessToken',
        ordered_params=[u'name'],
        path_params=[u'name'],
        query_params=[],
        relative_path=u'v1/{+name}:generateIdentityBindingAccessToken',
        request_field=u'generateIdentityBindingAccessTokenRequest',
        request_type_name=u'IamcredentialsProjectsServiceAccountsGenerateIdentityBindingAccessTokenRequest',
        response_type_name=u'GenerateIdentityBindingAccessTokenResponse',
        supports_download=False,
    )

    def SignBlob(self, request, global_params=None):
      r"""Signs a blob using a service account's system-managed private key.

      Args:
        request: (IamcredentialsProjectsServiceAccountsSignBlobRequest) input message
        global_params: (StandardQueryParameters, default: None) global arguments
      Returns:
        (SignBlobResponse) The response message.
      """
      config = self.GetMethodConfig('SignBlob')
      return self._RunMethod(
          config, request, global_params=global_params)

    SignBlob.method_config = lambda: base_api.ApiMethodInfo(
        flat_path=u'v1/projects/{projectsId}/serviceAccounts/{serviceAccountsId}:signBlob',
        http_method=u'POST',
        method_id=u'iamcredentials.projects.serviceAccounts.signBlob',
        ordered_params=[u'name'],
        path_params=[u'name'],
        query_params=[],
        relative_path=u'v1/{+name}:signBlob',
        request_field=u'signBlobRequest',
        request_type_name=u'IamcredentialsProjectsServiceAccountsSignBlobRequest',
        response_type_name=u'SignBlobResponse',
        supports_download=False,
    )

    def SignJwt(self, request, global_params=None):
      r"""Signs a JWT using a service account's system-managed private key.

      Args:
        request: (IamcredentialsProjectsServiceAccountsSignJwtRequest) input message
        global_params: (StandardQueryParameters, default: None) global arguments
      Returns:
        (SignJwtResponse) The response message.
      """
      config = self.GetMethodConfig('SignJwt')
      return self._RunMethod(
          config, request, global_params=global_params)

    SignJwt.method_config = lambda: base_api.ApiMethodInfo(
        flat_path=u'v1/projects/{projectsId}/serviceAccounts/{serviceAccountsId}:signJwt',
        http_method=u'POST',
        method_id=u'iamcredentials.projects.serviceAccounts.signJwt',
        ordered_params=[u'name'],
        path_params=[u'name'],
        query_params=[],
        relative_path=u'v1/{+name}:signJwt',
        request_field=u'signJwtRequest',
        request_type_name=u'IamcredentialsProjectsServiceAccountsSignJwtRequest',
        response_type_name=u'SignJwtResponse',
        supports_download=False,
    )

  class ProjectsService(base_api.BaseApiService):
    """Service class for the projects resource."""

    _NAME = u'projects'

    def __init__(self, client):
      super(IamcredentialsV1.ProjectsService, self).__init__(client)
      self._upload_configs = {
          }
