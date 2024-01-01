using System;
using System.Collections.Generic;
using TripServiceKata.Exception;
using TripServiceKata.User;

namespace TripServiceKata.Trip
{
    public class TripService
    {
        private readonly TripDAO tripDAO;
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
            bool isFriend = loggedInUser.IsFriend(user);
            var tripList = isFriend ? FindTripByUser(user) : NoTrip();
            return tripList;
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
