using System;
using System.Collections.Generic;
using TripServiceKata.Exception;
using TripServiceKata.User;

namespace TripServiceKata.Trip
{
    public class TripService
    {
        private TripDAO tripDAO;
        public TripService()
        {
            this.tripDAO = new TripDAO();
        }
        public TripService(TripDAO tripDAO)
        {
            this.tripDAO = tripDAO;
        }

        public List<Trip> GetTripsByUser(User.User user,User.User loggedInUser)
        {
            ValidateLoggedInUser(loggedInUser);
            bool isFriend = IsFriend(user, loggedInUser);
            var tripList = isFriend ? FindTripByUser(user) : NoTrip();
            return tripList;
        }

        private static bool IsFriend(User.User user, User.User loggedUser)
        {
            bool isFriend = false;
            foreach (User.User friend in user.GetFriends())
            {
                if (friend.Equals(loggedUser))
                {
                    isFriend = true;
                    break;
                }
            }
            return isFriend;
        }
        private static List<Trip> NoTrip()
        {
            return new List<Trip>();
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
            return tripDAO.tripsBy(user);
        }

        protected virtual User.User GetLoggedUser()
        {
            return UserSession.GetInstance().GetLoggedUser();
        }
    }
}
