const baseUrl = '/api/video';

export const getAllVideos = () => {
  console.log()
    return fetch(`${baseUrl}/GetWithComments`)
    .then((res) => res.json())
};

//searches for a video in the database
export const searchVideos = (search) => {
    return fetch(`${baseUrl}/search/?q=${search}`)
    .then((res) => res.json())
};

export const addVideo = (video) => {
  return fetch(baseUrl, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(video),
  });
};