const baseUrl = '/api/video';
const userUrl = '/api/userprofile/GetVideosByUser';
// const baseUrl2 = 'http:localhost:3000';

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

export const getVideo = (id) => {
    return fetch(`${baseUrl}/GetVideoWithComments/${id}`).then((res) => res.json());
};

export const users = (id) => {
    return fetch(`${userUrl}/${id}`).then((res) => res.json());
};

//OPTION 1    http://localhost:3000/users/1
//OPTION 2    http://localhost:3000/api/video/users/1
//OPTION 3    http://localhost:3000/api/video/
