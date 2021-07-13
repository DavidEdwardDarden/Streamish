import React from "react";
import { Switch, Route } from "react-router-dom";
import VideoList from "./VideoList";
import VideoForm from "./VideoForm";
import VideoDetails from "./VideoDetails";
import UserVideos from "./UserVideos";

export const ApplicationViews = () => {
  return (
    <Switch>
      <Route path="/" exact>
        <VideoList />
      </Route>

      <Route path="/videos/add">
        <VideoForm />
      </Route>

      <Route path="/videos/:id"> 
      <VideoDetails />
      </Route>
{/* below is React route which is different from the API route */}
      <Route path="/users/:id"> 
      <UserVideos />
      </Route>
    </Switch>
  );
};

export default ApplicationViews;