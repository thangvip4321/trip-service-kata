using System;
using System.Collections.Generic;
using TripServiceKata.Exception;
using TripServiceKata.User;

namespace TripServiceKata.Trip
{
    public class TripService
    {
        public List<Trip> GetTripsByUser(User.User user)
        {
            List<Trip> tripList = new List<Trip>();
            User.User loggedUser = GetLoggedUser();
            ValidateLoggedInUser(loggedUser);
            bool isFriend = false;
            foreach (User.User friend in user.GetFriends())
            {
                if (friend.Equals(loggedUser))
                {
                    isFriend = true;
                    break;
                }
            }
            if (isFriend)
            {
                tripList = FindTripByUser(user);
            }
            return tripList;
        }

        private static void ValidateLoggedInUser(User.User loggedUser)
        {
            if (loggedUser == null)
            {
                throw new UserNotLoggedInException();
            }
        }

        protected virtual List<Trip> FindTripByUser(User.User user)
        {
            return TripDAO.FindTripsByUser(user);
        }

        protected virtual User.User GetLoggedUser()
        {
            return UserSession.GetInstance().GetLoggedUser();
        }
    }
}
