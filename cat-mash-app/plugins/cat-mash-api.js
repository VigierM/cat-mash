export default function ({ $axios, error: nuxtError }, inject) {
    const catMashAPI = $axios.create({
      headers: {
        common: {
          Accept: 'text/plain, */*'
        }
      }
    })

    $axios.onError((error) => {
      if (error.response) {
        nuxtError({
          statusCode: error.response.status,
          message: error.message
        })
      } else {
        nuxtError({
          statusCode: error.status,
          message: error.message
        })
      }

      return Promise.resolve(false)
    })

    catMashAPI.setBaseURL('https://localhost:7141/api/cats/')

    inject('api', catMashAPI)
  }
